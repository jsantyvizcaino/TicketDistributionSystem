# TicketDistributionSystem

Sistema de gestión y distribución de ítems de trabajo basado en arquitectura de microservicios.

## Arquitectura

El sistema está compuesto por dos microservicios independientes, cada uno con su propia base de datos, siguiendo los principios de Clean Architecture y CQRS.

### MsUser (Puerto 5188)
Gestiona las referencias de usuarios y sus estadísticas de carga de trabajo. Los usuarios existen en un sistema externo; este microservicio solo mantiene la referencia del username y el seguimiento de ítems asignados, completados, pendientes y de alta relevancia.

### MsItem (Puerto 5189)
Gestiona los ítems de trabajo y ejecuta el algoritmo de distribución. Se comunica con MsUser vía HTTP para obtener usuarios disponibles y actualizar su carga de trabajo.

## Algoritmo de Distribución

La distribución se ejecuta mediante el Strategy Pattern con tres estrategias evaluadas en orden de prioridad:

1. **UrgentStrategy**: Si la fecha de entrega es menor a 3 días, se asigna al usuario con menos ítems totales, independientemente de la relevancia.
2. **HighRelevanceStrategy**: Los ítems de alta relevancia se asignan al usuario con menos pendientes.
3. **DefaultStrategy**: Los ítems de baja relevancia se distribuyen al usuario con menor carga total.

Reglas adicionales:
- Un usuario con 3 o más ítems de alta relevancia se considera saturado y se excluye de la distribución.
- Después de cada asignación, la lista de pendientes del usuario se ordena por fecha de entrega y relevancia.

## Tecnologías

- .NET 9 / C#
- Entity Framework Core 9 (Code First)
- SQL Server 2022
- Docker / Docker Compose
- Mediator (CQRS con Source Generators)
- Refit (Comunicación HTTP entre microservicios)
- Clean Architecture (Domain, Application, Infrastructure, API)

## Patrones de Diseño

- **CQRS**: Separación de Commands y Queries con Mediator.
- **Strategy Pattern**: Algoritmo de distribución con estrategias intercambiables.
- **Factory Pattern**: Selección configurable del proveedor HTTP (Refit o HttpClient nativo) desde appsettings.json.
- **Repository Pattern**: Repositorio genérico base con implementaciones específicas.

## Estructura del Proyecto


    TicketDistributionSystem/
    ├── docker-compose.yml
    ├── TicketDistributionSystem.sln
    └── Services/
        ├── MsUser/
        │   ├── MsUser.API/
        │   ├── MsUser.Application/
        │   ├── MsUser.Infrastructure/
        │   └── MsUser.Domain/
        └── MsItem/
            ├── MsItem.API/
            ├── MsItem.Application/
            ├── MsItem.Infrastructure/
            └── MsItem.Domain/


## Requisitos Previos

- .NET 9 SDK
- Docker

## Ejecución

### Con Docker Compose (recomendado)

```bash
docker-compose up --build
```

Esto levanta:
- SQL Server en puerto 1433
- MsUser API en http://localhost:5188
- MsItem API en http://localhost:5189



## Endpoints Principales

### MsUser

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /api/users | Listar todos los usuarios |
| GET | /api/users/{username} | Obtener usuario por username |
| GET | /api/users/available | Usuarios disponibles para distribución |
| POST | /api/users | Crear usuario |
| PUT | /api/users/{username}/workload | Actualizar carga de trabajo |

### MsItem

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /api/workitems | Listar todos los ítems |
| GET | /api/workitems/{id} | Obtener ítem por ID |
| GET | /api/workitems/pending/{username} | Pendientes ordenados por usuario |
| POST | /api/workitems | Crear ítem de trabajo |
| POST | /api/workitems/{id}/distribute | Distribuir ítem a un usuario |

## Configuración del Proveedor HTTP

El proveedor de comunicación HTTP entre microservicios es configurable en `appsettings.json` de MsItem:

```json
{
  "HttpClient": {
    "Provider": "refit"
  }
}
```

Opciones disponibles: `refit` o `native`.

## Flujo de Distribución - Ejemplo Práctico

### Estado inicial de usuarios precargados

```bash
curl http://localhost:5188/api/users
```

| Usuario | Asignados | Pendientes | Alta Relevancia | Saturado |
|---------|-----------|------------|-----------------|----------|
| juan.perez | 0 | 0 | 0 | No |
| maria.garcia | 0 | 0 | 0 | No |
| carlos.lopez | 0 | 0 | 0 | No |

### Ítems de trabajo precargados

```bash
curl http://localhost:5189/api/workitems
```

| ID | Título | Relevancia | Vence en | Estrategia esperada |
|----|--------|------------|----------|---------------------|
| aaaaaaaa-3333... | Corregir error en distribución | Alta | 1 día | UrgentStrategy |
| aaaaaaaa-1111... | Revisión de facturación mensual | Alta | 2 días | UrgentStrategy |
| aaaaaaaa-5555... | Migrar base de datos | Alta | 3 días | HighRelevanceStrategy |
| aaaaaaaa-4444... | Preparar reporte de indicadores | Baja | 5 días | DefaultStrategy |
| aaaaaaaa-2222... | Actualizar documentación de API | Baja | 7 días | DefaultStrategy |

### Paso 1: Distribuir ítem urgente (vence en 1 día)

```bash
curl -X POST http://localhost:5189/api/workitems/aaaaaaaa-3333-3333-3333-333333333333/distribute
```

**Estrategia:** UrgentStrategy (fecha < 3 días)
**Resultado:** Asignado al usuario con menos ítems totales (todos tienen 0, se toma el primero)

| Usuario | Asignados | Pendientes | Alta Relevancia |
|---------|-----------|------------|-----------------|
| juan.perez | 1 | 1 | 1 |
| maria.garcia | 0 | 0 | 0 |
| carlos.lopez | 0 | 0 | 0 |

### Paso 2: Distribuir ítem urgente (vence en 2 días)

```bash
curl -X POST http://localhost:5189/api/workitems/aaaaaaaa-1111-1111-1111-111111111111/distribute
```

**Estrategia:** UrgentStrategy (fecha < 3 días)
**Resultado:** Asignado al usuario con menos ítems totales → maria.garcia o carlos.lopez

| Usuario | Asignados | Pendientes | Alta Relevancia |
|---------|-----------|------------|-----------------|
| juan.perez | 1 | 1 | 1 |
| maria.garcia | 1 | 1 | 1 |
| carlos.lopez | 0 | 0 | 0 |

### Paso 3: Distribuir ítem alta relevancia (vence en 3 días)

```bash
curl -X POST http://localhost:5189/api/workitems/aaaaaaaa-5555-5555-5555-555555555555/distribute
```

**Estrategia:** HighRelevanceStrategy (relevancia alta, fecha >= 3 días)
**Resultado:** Asignado al usuario con menos pendientes → carlos.lopez

| Usuario | Asignados | Pendientes | Alta Relevancia |
|---------|-----------|------------|-----------------|
| juan.perez | 1 | 1 | 1 |
| maria.garcia | 1 | 1 | 1 |
| carlos.lopez | 1 | 1 | 1 |

### Paso 4: Distribuir ítem baja relevancia (vence en 5 días)

```bash
curl -X POST http://localhost:5189/api/workitems/aaaaaaaa-4444-4444-4444-444444444444/distribute
```

**Estrategia:** DefaultStrategy (relevancia baja)
**Resultado:** Asignado al usuario con menos ítems totales (balanceado)

### Paso 5: Verificar ordenamiento de pendientes

```bash
curl http://localhost:5189/api/workitems/pending/juan.perez
```

Los pendientes se devuelven ordenados por:
1. Fecha de entrega (más próxima primero)
2. Relevancia (alta antes que baja)

### Paso 6: Verificar saturación

Si un usuario acumula 3 o más ítems de alta relevancia, queda excluido:

```bash
curl http://localhost:5188/api/users/available
```

Un usuario con `highRelevanceItems >= 3` aparece con `isSaturated: true` y no recibe más ítems.

## Autor

Santiago Vizcaíno
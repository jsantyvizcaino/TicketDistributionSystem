using Microsoft.EntityFrameworkCore;
using MsItem.Domain.Entities;
using MsItem.Domain.Enums;

namespace MsItem.Infrastructure.Persistence.Seed;

/// <summary>
/// Datos semilla de ítems de trabajo de ejemplo para el entorno de desarrollo.
/// </summary>
public static class WorkItemSeedData
{
    /// <summary>
    /// Inserta ítems de trabajo de ejemplo si la tabla está vacía (operación idempotente).
    /// </summary>
    public static async Task EnsureDefaultWorkItemsAsync(AppDbContext context, CancellationToken ct = default)
    {
        if (await context.WorkItems.AnyAsync(ct))
            return;

        var workItems = new List<WorkItem>
        {
            new()
            {
                Id = Guid.Parse("aaaaaaaa-1111-1111-1111-111111111111"),
                Title = "Revisión de facturación mensual",
                Description = "Validar las facturas emitidas durante el mes actual",
                Relevance = RelevanceLevel.High,
                Status = WorkItemStatus.Pending,
                DueDate = DateTime.UtcNow.AddDays(2),
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("aaaaaaaa-2222-2222-2222-222222222222"),
                Title = "Actualizar documentación de API",
                Description = "Completar la documentación de los nuevos endpoints",
                Relevance = RelevanceLevel.Low,
                Status = WorkItemStatus.Pending,
                DueDate = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("aaaaaaaa-3333-3333-3333-333333333333"),
                Title = "Corregir error en distribución de ítems",
                Description = "Resolver el bug reportado en el algoritmo de asignación",
                Relevance = RelevanceLevel.High,
                Status = WorkItemStatus.Pending,
                DueDate = DateTime.UtcNow.AddDays(1),
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("aaaaaaaa-4444-4444-4444-444444444444"),
                Title = "Preparar reporte de indicadores",
                Description = "Generar el reporte semanal de indicadores de gestión",
                Relevance = RelevanceLevel.Low,
                Status = WorkItemStatus.Pending,
                DueDate = DateTime.UtcNow.AddDays(5),
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("aaaaaaaa-5555-5555-5555-555555555555"),
                Title = "Migrar base de datos a nueva versión",
                Description = "Ejecutar la migración de esquema en el ambiente de pruebas",
                Relevance = RelevanceLevel.High,
                Status = WorkItemStatus.Pending,
                DueDate = DateTime.UtcNow.AddDays(3),
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.WorkItems.AddRangeAsync(workItems, ct);
        await context.SaveChangesAsync(ct);
    }
}

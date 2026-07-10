namespace MsUser.Domain.Entities;

/// <summary>
/// Clase base para todas las entidades del dominio
/// Contiene propiedades de auditoría comunes
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Identificador único de la entidad.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Fecha y hora (UTC) en la que se creó el registro.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Fecha y hora (UTC) de la última actualización del registro, si existe.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
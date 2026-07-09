namespace MsItem.Domain.Entities;

/// <summary>
/// Clase base para todas las entidades del dominio
/// Contiene propiedades de auditoría comunes
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
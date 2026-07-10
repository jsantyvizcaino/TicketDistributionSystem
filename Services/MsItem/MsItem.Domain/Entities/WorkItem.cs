using MsItem.Domain.Enums;

namespace MsItem.Domain.Entities;

/// <summary>
/// Entidad WorkItem - Representa un ítem de trabajo a distribuir
/// </summary>
public class WorkItem : BaseEntity
{
    /// <summary>
    /// Título del ítem de trabajo.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Descripción detallada del ítem de trabajo.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Nombre de usuario al que está asignado el ítem, o <c>null</c> si aún no ha sido distribuido.
    /// No es una clave foránea: el usuario reside en el microservicio MsUser.
    /// </summary>
    public string? AssignedUsername { get; set; }

    /// <summary>
    /// Nivel de relevancia del ítem, usado por las estrategias de distribución.
    /// </summary>
    public RelevanceLevel Relevance { get; set; }

    /// <summary>
    /// Estado actual del ítem dentro de su ciclo de vida.
    /// </summary>
    public WorkItemStatus Status { get; set; }

    /// <summary>
    /// Fecha límite para completar el ítem.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Fecha y hora (UTC) en la que el ítem fue asignado a un usuario, si aplica.
    /// </summary>
    public DateTime? AssignedAt { get; set; }

    /// <summary>
    /// Fecha y hora (UTC) en la que el ítem fue marcado como completado, si aplica.
    /// </summary>
    public DateTime? CompletedAt { get; set; }
}

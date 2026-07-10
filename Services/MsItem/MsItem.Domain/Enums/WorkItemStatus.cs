namespace MsItem.Domain.Enums;

/// <summary>
/// Estados posibles en el ciclo de vida de un ítem de trabajo.
/// </summary>
public enum WorkItemStatus
{
    /// <summary>
    /// El ítem fue creado pero aún no ha sido asignado a ningún usuario.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// El ítem fue asignado a un usuario y está siendo trabajado.
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// El ítem fue finalizado por el usuario asignado.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// El ítem fue cancelado y no requiere más seguimiento.
    /// </summary>
    Cancelled = 3
}

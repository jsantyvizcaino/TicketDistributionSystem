namespace MsUser.Domain.Entities;


/// <summary>
/// Entidad User - Representa un usuario del sistema
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Nombre de usuario único, utilizado como identificador de referencia desde MsItem.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Correo electrónico del usuario.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Nombre completo del usuario.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Indica si el usuario está activo y puede recibir ítems de trabajo.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Cantidad total de ítems de trabajo asignados al usuario históricamente.
    /// </summary>
    public int TotalAssignedItems { get; set; } = 0;

    /// <summary>
    /// Cantidad de ítems de trabajo completados por el usuario.
    /// </summary>
    public int CompletedItems { get; set; } = 0;

    /// <summary>
    /// Cantidad de ítems de trabajo actualmente pendientes o en progreso para el usuario.
    /// </summary>
    public int PendingItems { get; set; } = 0;

    /// <summary>
    /// Cantidad de ítems de alta relevancia actualmente asignados al usuario.
    /// </summary>
    public int HighRelevanceItems { get; set; } = 0;

    /// <summary>
    /// Determina si el usuario está saturado.
    /// Un usuario se considera saturado cuando tiene 3 o más ítems de alta relevancia asignados.
    /// Los usuarios saturados son excluidos del proceso de distribución.
    /// </summary>
    public bool IsSaturated() => HighRelevanceItems >= 3;
}

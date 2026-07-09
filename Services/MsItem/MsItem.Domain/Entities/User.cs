namespace MsItem.Domain.Entities;


/// <summary>
/// Entidad User - Representa un usuario del sistema
/// </summary>
public class User : BaseEntity
{
    public required string Username { get; set; }      
    public string? Email { get; set; }
    public required string FullName { get; set; }
    public bool IsActive { get; set; }

    public int TotalAssignedItems { get; set; } = 0;
    public int CompletedItems { get; set; } = 0;
    public int PendingItems { get; set; } = 0;
    public int HighRelevanceItems { get; set; } = 0;

    public bool IsSaturated() => HighRelevanceItems >= 3;
}

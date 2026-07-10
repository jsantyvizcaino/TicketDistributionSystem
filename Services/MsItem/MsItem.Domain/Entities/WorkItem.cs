using MsItem.Domain.Enums;

namespace MsItem.Domain.Entities;

/// <summary>
/// Entidad WorkItem - Representa un ítem de trabajo a distribuir
/// </summary>
public class WorkItem : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? AssignedUsername { get; set; }
    public RelevanceLevel Relevance { get; set; }
    public WorkItemStatus Status { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? AssignedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

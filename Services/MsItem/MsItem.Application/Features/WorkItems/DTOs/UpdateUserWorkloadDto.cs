namespace MsItem.Application.Features.WorkItems.DTOs;
public sealed record UpdateUserWorkloadDto(
    string Username,
    int IncrementAssigned,
    int IncrementPending,
    int IncrementHighRelevance,
    bool IsCompletion
);

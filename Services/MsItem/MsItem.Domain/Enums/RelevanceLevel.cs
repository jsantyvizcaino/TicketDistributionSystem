namespace MsItem.Domain.Enums;

/// <summary>
/// Nivel de relevancia de un ítem de trabajo, utilizado por las estrategias de distribución
/// para priorizar la asignación.
/// </summary>
public enum RelevanceLevel
{
    /// <summary>
    /// Relevancia baja: se distribuye con la estrategia de respaldo (<c>DefaultStrategy</c>).
    /// </summary>
    Low = 0,

    /// <summary>
    /// Relevancia alta: se distribuye priorizando a usuarios con menos ítems pendientes
    /// (<c>HighRelevanceStrategy</c>), salvo que además sea urgente.
    /// </summary>
    High = 1
}

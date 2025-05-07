using JMLS.Domain.InteractionTypes;

namespace JMLS.Domain.Points;

public class PointsEarned : PointTransaction
{
    public int InteractionTypeId { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public required InteractionType InteractionType { get; set; }
}
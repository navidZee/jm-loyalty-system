using JMLS.Domain.InteractionTypes;

namespace JMLS.Domain.Points;

public class PointEarned : PointTransaction
{
    private PointEarned()
    {
    }

    public PointEarned(InteractionType interactionType)
    {
        InteractionTypeId = interactionType.Id;
        if (interactionType.ExpirationPeriod.HasValue)
        {
            ExpirationDate = DateTime.Now.Add(interactionType.ExpirationPeriod.Value);
        }
        Amount = interactionType.Amount;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int InteractionTypeId { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    
    public InteractionType InteractionType { get; set; } = null!;
    public bool IsActive => ExpirationDate is null || ExpirationDate.Value.Date >= DateTime.Now.Date; 
}

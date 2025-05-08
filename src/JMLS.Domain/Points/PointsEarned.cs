using JMLS.Domain.Activities;
using JMLS.Domain.Points.Rules;

namespace JMLS.Domain.Points;

public class PointEarned : PointTransaction
{
    private PointEarned()
    {
    }

    public PointEarned(Activity activity)
    {
        CheckRules(new ActivityMustBeProvidedRule(activity));
        
        InteractionTypeId = activity.Id;
        if (activity.ExpirationPeriod.HasValue)
        {
            ExpirationDate = DateTime.Now.Add(activity.ExpirationPeriod.Value);
        }
        PointsValue = activity.PointsEarned;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int InteractionTypeId { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    
    public Activity Activity { get; set; } = null!;
    public bool IsActive => ExpirationDate is null || ExpirationDate.Value.Date >= DateTime.Now.Date; 
}

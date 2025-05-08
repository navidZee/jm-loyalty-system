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
        
        ActivityId = activity.Id;
        if (activity.ExpirationPeriod.HasValue)
        {
            ExpirationDate = DateTime.Now.Add(activity.ExpirationPeriod.Value);
        }
        PointValue = activity.PointsReward;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int ActivityId { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public Activity Activity { get; set; } = null!;
    public bool IsActive => ExpirationDate is null || ExpirationDate.Value.Date >= DateTime.Now.Date; 
}

using JMLS.Domain.Activities;

namespace JMLS.Domain.Points;

public class PointEarned : PointTransaction
{
    private PointEarned()
    {
    }

    internal PointEarned(Activity activity, int referenceId)
    {
        ActivityId = activity.Id;
        ReferenceId = referenceId;
        if (activity.ExpirationPeriod.HasValue)
        {
            ExpirationDate = DateTime.Now.Add(activity.ExpirationPeriod.Value);
        }
        PointValue = activity.PointsReward;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int ReferenceId { get; private set; }
    public int ActivityId { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public Activity Activity { get; set; } = null!;
    public bool IsActive => ExpirationDate is null || ExpirationDate.Value.Date >= DateTime.Now.Date; 
}

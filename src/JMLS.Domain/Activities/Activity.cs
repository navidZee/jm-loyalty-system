using JMLS.Domain.Activities.Rules;
using JMLS.Domain.Points;

namespace JMLS.Domain.Activities;

public class Activity : EntityBase
{
    private Activity()
    {
        
    }

    public Activity(string title, ActivityType activityType, int pointsReward, string description,
        TimeSpan? expirationPeriod)
    {
        CheckRules(
            new TitleMustBeProvidedRule(title),
            new PointsRewardMustBeMoreThanZeroRule(pointsReward),
            new DescriptionMustBeProvidedRule(description));

        Title = title;
        ActivityType = activityType;
        Description = description;
        PointsReward = pointsReward;
        ExpirationPeriod = expirationPeriod;
        DateModified = DateTime.Now;
        DateCreated = DateTime.Now;
    }

    public int Id { get; set; }
    public string Title { get; private set; }
    public ActivityType ActivityType { get; private set; }
    public int PointsReward { get; private set; }
    public string Description { get; private set; }
    public TimeSpan? ExpirationPeriod { get; private set; }
    public  IReadOnlyList<PointEarned> PointsEarned { get; set; }
}

public enum ActivityType
{
    Purchases,
    SocialMedia,
    Referrals
}
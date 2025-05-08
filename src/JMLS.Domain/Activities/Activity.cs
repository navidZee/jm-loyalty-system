using JMLS.Domain.Activities.Rules;

namespace JMLS.Domain.Activities;

public class Activity : EntityBase
{
    private Activity()
    {
    }

    public Activity(string title, ActivityType activityType, int pointsEarned, string description,
        TimeSpan? expirationPeriod)
    {
        CheckRules(
            new TitleMustBeProvidedRule(title),
            new PointsEarnedMustBeMoreThanZeroRule(pointsEarned),
            new DescriptionMustBeProvidedRule(description));

        Title = title;
        ActivityType = activityType;
        Description = description;
        PointsEarned = pointsEarned;
        ExpirationPeriod = expirationPeriod;
        DateModified = DateTime.Now;
        DateCreated = DateTime.Now;
    }

    public int Id { get; set; }
    public string Title { get; private set; }
    public ActivityType ActivityType { get; private set; }
    public int PointsEarned { get; private set; }
    public string Description { get; private set; }
    public TimeSpan? ExpirationPeriod { get; private set; }
}

public enum ActivityType
{
    Purchases,
    SocialMedia,
    Referrals
}
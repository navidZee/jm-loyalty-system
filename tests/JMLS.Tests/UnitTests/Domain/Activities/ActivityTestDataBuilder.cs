using JMLS.Domain.Activities;

namespace JMLS.Tests.UnitTests.Domain.Activities;

public class ActivityTestDataBuilder
{
    private string _title = "Test Title";
    private ActivityType _activityType = ActivityType.Purchases;
    private string _description = "Test Description";
    private int _pointsReward = 100;
    private TimeSpan? _expirationPeriod = TimeSpan.FromDays(30);

    public ActivityTestDataBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public ActivityTestDataBuilder WithActivityType(ActivityType activityType)
    {
        _activityType = activityType;
        return this;
    }

    public ActivityTestDataBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public ActivityTestDataBuilder WithPointsReward(int pointsReward)
    {
        _pointsReward = pointsReward;
        return this;
    }

    public ActivityTestDataBuilder WithExpirationPeriod(TimeSpan? expirationPeriod)
    {
        _expirationPeriod = expirationPeriod;
        return this;
    }

    public Activity Build()
    {
        return new Activity(_title, _activityType, _pointsReward, _description, _expirationPeriod)
        {
            PointsEarned = []
        };
    }
}
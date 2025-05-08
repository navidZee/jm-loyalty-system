using JMLS.Domain.Activities;

namespace JMLS.Tests.UnitTests.Domain.InteractionTypes;

public class InteractionTypeTestDataBuilder
{
    private string _title = "Test Title";
    private ActivityType _activityType = ActivityType.Purchases;
    private string _description = "Test Description";
    private int _pointsEarned = 100;
    private TimeSpan? _expirationPeriod = TimeSpan.FromDays(30);

    public InteractionTypeTestDataBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public InteractionTypeTestDataBuilder WithActivityType(ActivityType activityType)
    {
        _activityType = activityType;
        return this;
    }

    public InteractionTypeTestDataBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public InteractionTypeTestDataBuilder WithPointsEarned(int pointsEarned)
    {
        _pointsEarned = pointsEarned;
        return this;
    }

    public InteractionTypeTestDataBuilder WithExpirationPeriod(TimeSpan? expirationPeriod)
    {
        _expirationPeriod = expirationPeriod;
        return this;
    }

    public Activity Build()
    {
        return new Activity(_title, _activityType, _pointsEarned, _description, _expirationPeriod);
    }
}
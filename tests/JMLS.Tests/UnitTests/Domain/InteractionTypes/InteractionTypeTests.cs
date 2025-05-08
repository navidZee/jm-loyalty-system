using JMLS.Domain;
using JMLS.Domain.Activities;

namespace JMLS.Tests.UnitTests.Domain.InteractionTypes;

public class ActivityTests
{
    private readonly InteractionTypeTestDataBuilder _builder = new();

    [Fact]
    public void Constructor_WhenValidParametersProvided_ThenInteractionTypeIsCreated()
    {
        // Arrange
        const string title = "Valid Title";
        const string description = "Valid Description";
        const ActivityType activityType = ActivityType.SocialMedia;
        const int pointsEarned = 50;
        var expirationPeriod = TimeSpan.FromDays(30);

        // Act
        var activity = _builder
            .WithTitle(title)
            .WithActivityType(activityType)
            .WithDescription(description)
            .WithPointsEarned(pointsEarned)
            .WithExpirationPeriod(expirationPeriod)
            .Build();

        // Assert
        Assert.NotNull(activity);
        Assert.Equal(title, activity.Title);
        Assert.Equal(activityType, activity.ActivityType);
        Assert.Equal(description, activity.Description);
        Assert.Equal(pointsEarned, activity.PointsEarned);
        Assert.Equal(expirationPeriod, activity.ExpirationPeriod);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_WhenKeyIsInvalid_ThenThrowsBusinessRuleValidationException(string title)
    {
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => _builder.WithTitle(title).Build()
        );
        
        Assert.Equal("Title must be provided.", exception.Message);
    }

    [Fact]
    public void Constructor_WhenKeyIsNull_ThenThrowsBusinessRuleValidationException()
    {
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => _builder.WithTitle(null!).Build()
        );
        
        Assert.Equal("Title must be provided.", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_WhenDescriptionIsInvalid_ThenThrowsBusinessRuleValidationException(string description)
    {
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => _builder.WithDescription(description).Build()
        );
        
        Assert.Equal("Description must be provided.", exception.Message);
    }

    [Fact]
    public void Constructor_WhenDescriptionIsNull_ThenThrowsBusinessRuleValidationException()
    {
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => _builder.WithDescription(null!).Build()
        );
        
        Assert.Equal("Description must be provided.", exception.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_WhenPointsEarnedIsNotPositive_ThenThrowsBusinessRuleValidationException(int pointsEarned)
    {
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => _builder.WithPointsEarned(pointsEarned).Build()
        );
        
        Assert.Equal("The points earned must be more than zero.", exception.Message);
    }

    [Fact]
    public void Constructor_WhenExpirationPeriodIsNull_ThenInteractionTypeIsCreatedWithoutExpiration()
    {
        // Act
        var activity = _builder.WithExpirationPeriod(null).Build();

        // Assert
        Assert.Null(activity.ExpirationPeriod);
    }
}
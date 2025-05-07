using JMLS.Domain;
using JMLS.Domain.InteractionTypes;
using JMLS.Domain.Points;
using JMLS.Domain.Rewards;
using JMLS.Tests.UnitTests.Domain.InteractionTypes;
using JMLS.Tests.UnitTests.Domain.Rewards;

namespace JMLS.Tests.UnitTests.Domain.Points;

public class PointTests
{
    private readonly PointTestDataBuilder _pointBuilder = new();
    private readonly InteractionTypeTestDataBuilder _interactionTypeBuilder = new();
    private readonly RewardTestDataBuilder _rewardBuilder = new();

    [Fact]
    public void Constructor_WhenValidCustomerId_ThenPointIsCreated()
    {
        // Arrange
        const int customerId = 100;

        // Act
        var point = _pointBuilder.WithCustomerId(customerId).Build();

        // Assert
        Assert.NotNull(point);
        Assert.Equal(customerId, point.CustomerId);
        Assert.Equal(0, point.Balance);
        Assert.NotNull(point.PointsEarned);
        Assert.NotNull(point.PointsSpent);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_WhenCustomerIdIsInvalid_ThenThrowsBusinessRuleValidationException(int customerId)
    {
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => _pointBuilder.WithCustomerId(customerId).Build()
        );

        Assert.Equal("Customer Id must be more than zero.", exception.Message);
    }

    [Fact]
    public void Earned_WhenValidInteractionType_ThenPointsAreAdded()
    {
        // Arrange
        var point = _pointBuilder.Build();
        var interactionType = _interactionTypeBuilder
            .WithAmount(50.0m)
            .WithExpirationPeriod(TimeSpan.FromDays(30))
            .Build();

        // Act
        point.Earned(interactionType);

        // Assert
        Assert.Single(point.PointsEarned);
        Assert.Equal(50.0m, point.Balance);
        Assert.Equal(interactionType.Id, point.PointsEarned[0].InteractionTypeId);
        Assert.NotNull(point.PointsEarned[0].ExpirationDate);
    }

    [Fact]
    public void Earned_WhenInteractionTypeWithoutExpiration_ThenPointsAddedWithoutExpiration()
    {
        // Arrange
        var point = _pointBuilder.Build();
        var interactionType = _interactionTypeBuilder
            .WithAmount(50.0m)
            .WithExpirationPeriod(null)
            .Build();

        // Act
        point.Earned(interactionType);

        // Assert
        Assert.Single(point.PointsEarned);
        Assert.Equal(50.0m, point.Balance);
        Assert.Null(point.PointsEarned[0].ExpirationDate);
    }

    [Fact]
    public void Earned_WhenInteractionTypeIsInvalid_ThenThrowsBusinessRuleValidationException()
    {
        // Arrange
        var point = _pointBuilder.Build();

        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => point.Earned(null!)
        );

        Assert.Equal("Interaction type must be provided", exception.Message);
    }

    [Fact]
    public void Spent_WhenValidReward_ThenPointsAreDeducted()
    {
        // Arrange
        var point = _pointBuilder.Build();
        var interactionType = _interactionTypeBuilder
            .WithAmount(100.0m)
            .Build();
        var reward = _rewardBuilder
            .WithAmount(50.0m)
            .Build();

        point.Earned(interactionType);
        var initialBalance = point.Balance;

        // Act
        point.Spent(reward);

        // Assert
        Assert.Single(point.PointsSpent);
        Assert.Equal(initialBalance - reward.Amount, point.Balance);
        Assert.Equal(reward.Id, point.PointsSpent[0].RewardId);
    }

    [Fact]
    public void Spent_WhenRewardIsInvalid_ThenThrowsBusinessRuleValidationException()
    {
        // Arrange
        var point = _pointBuilder.Build();

        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => point.Spent(null!)
        );

        Assert.Equal("Reward must be provided", exception.Message);
    }

    [Fact]
    public void Balance_WhenPointsExpired_ThenBalanceExcludesExpiredPoints()
    {
        // Arrange
        var point = _pointBuilder.Build();
        var expiredInteractionType = _interactionTypeBuilder
            .WithAmount(50.0m)
            .WithExpirationPeriod(TimeSpan.FromDays(-1))
            .Build();
        var validInteractionType = _interactionTypeBuilder
            .WithAmount(100.0m)
            .WithExpirationPeriod(TimeSpan.FromDays(30))
            .Build();

        // Act
        point.Earned(expiredInteractionType);
        point.Earned(validInteractionType);

        // Assert
        Assert.Equal(2, point.PointsEarned.Count);
        Assert.Equal(100.0m, point.Balance); // Only non-expired points should be counted
    }
}
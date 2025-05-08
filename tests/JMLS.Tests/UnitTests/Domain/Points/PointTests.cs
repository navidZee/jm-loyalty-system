using JMLS.Domain;
using JMLS.Domain.Activities;
using JMLS.Domain.Points;
using JMLS.Domain.Offers;
using JMLS.Tests.UnitTests.Domain.InteractionTypes;
using JMLS.Tests.UnitTests.Domain.Offers;

namespace JMLS.Tests.UnitTests.Domain.Points;

public class PointTests
{
    private readonly PointTestDataBuilder _pointBuilder = new();
    private readonly InteractionTypeTestDataBuilder _activityBuilder = new();
    private readonly OfferTestDataBuilder _offerBuilder = new();

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
        var activity = _activityBuilder
            .WithPointsEarned(50)
            .WithExpirationPeriod(TimeSpan.FromDays(30))
            .Build();

        // Act
        point.Earned(activity);

        // Assert
        Assert.Single(point.PointsEarned);
        Assert.Equal(50.0m, point.Balance);
        Assert.Equal(activity.Id, point.PointsEarned[0].InteractionTypeId);
        Assert.NotNull(point.PointsEarned[0].ExpirationDate);
    }

    [Fact]
    public void Earned_WhenInteractionTypeWithoutExpiration_ThenPointsAddedWithoutExpiration()
    {
        // Arrange
        var point = _pointBuilder.Build();
        var activity = _activityBuilder
            .WithPointsEarned(50)
            .WithExpirationPeriod(null)
            .Build();

        // Act
        point.Earned(activity);

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
    public void Spent_WhenValidOffer_ThenPointsAreDeducted()
    {
        // Arrange
        var point = _pointBuilder.Build();
        var activity = _activityBuilder
            .WithPointsEarned(100)
            .Build();
        var offer = _offerBuilder
            .WithPointSpent(50)
            .WithPointSpent(50)
            .Build();

        point.Earned(activity);
        var initialBalance = point.Balance;

        // Act
        point.Spent(offer);

        // Assert
        Assert.Single(point.PointsSpent);
        Assert.Equal(initialBalance - offer.PointSpent, point.Balance);
        Assert.Equal(offer.Id, point.PointsSpent[0].OfferId);
    }

    [Fact]
    public void Spent_WhenOfferIsInvalid_ThenThrowsBusinessRuleValidationException()
    {
        // Arrange
        var point = _pointBuilder.Build();
        
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => point.Spent(null!)
        );

        Assert.Equal("Offer must be provided", exception.Message);
    }

    [Fact]
    public void Spent_WhenInsufficientBalance_ThenThrowsBusinessRuleValidationException()
    {
        // Arrange
        var point = _pointBuilder.Build();
        var activity = _activityBuilder
            .WithPointsEarned(50)
            .Build();
        var offer = _offerBuilder
            .WithPointSpent(100)
            .Build();

        point.Earned(activity); // Balance will be 50

        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => point.Spent(offer)
        );

        Assert.Equal("Insufficient point balance for this offer", exception.Message);
    }

    [Fact]
    public void Balance_WhenPointsExpired_ThenBalanceExcludesExpiredPoints()
    {
        // Arrange
        var point = _pointBuilder.Build();
        var expiredInteractionType = _activityBuilder
            .WithPointsEarned(50)
            .WithExpirationPeriod(TimeSpan.FromDays(-1))
            .Build();
        var validInteractionType = _activityBuilder
            .WithPointsEarned(100)
            .WithExpirationPeriod(TimeSpan.FromDays(30))
            .Build();

        // Act
        point.Earned(expiredInteractionType);
        point.Earned(validInteractionType);

        // Assert
        Assert.Equal(2, point.PointsEarned.Count);
        Assert.Equal(100, point.Balance); // Only non-expired points should be counted
    }

    [Fact]
    public void Balance_WhenMultipleTransactions_ThenBalanceIsCalculatedCorrectly()
    {
        // Arrange
        var point = _pointBuilder.Build();
        var activity1 = _activityBuilder.WithPointsEarned(100).Build();
        var activity2 = _activityBuilder.WithPointsEarned(50).Build();
        var offer = _offerBuilder.WithPointSpent(30).Build();

        // Act
        point.Earned(activity1); // +100
        point.Earned(activity2); // +50
        point.Spent(offer); // -30

        // Assert
        Assert.Equal(activity1.PointsEarned + activity2.PointsEarned - offer.PointSpent, point.Balance);
        Assert.Equal(2, point.PointsEarned.Count);
        Assert.Single(point.PointsSpent);
    }
}
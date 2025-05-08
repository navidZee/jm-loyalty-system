using JMLS.Domain;
using JMLS.Tests.UnitTests.Domain.Activities;
using JMLS.Tests.UnitTests.Domain.Offers;

namespace JMLS.Tests.UnitTests.Domain.Customers;

public class CustomerTests
{
    private readonly CustomerTestDataBuilder _customerBuilder = new();
    private readonly ActivityTestDataBuilder _activityBuilder = new();
    private readonly OfferTestDataBuilder _offerBuilder = new();

    [Fact]
    public void Constructor_WhenValidUsername_ThenCustomerIsCreated()
    {
        // Arrange
        const string username = "testuser";

        // Act
        var customer = _customerBuilder.WithUsername(username).Build();

        // Assert
        Assert.NotNull(customer);
        Assert.Equal(username, customer.Username);
        Assert.Equal(0, customer.PointBalance);
        Assert.NotNull(customer.PointsEarned);
        Assert.NotNull(customer.PointsSpent);
    }

    [Fact]
    public void Earned_WhenValidActivity_ThenPointsAreAdded()
    {
        // Arrange
        var customer = _customerBuilder.Build();
        var activity = _activityBuilder
            .WithPointsReward(50)
            .WithExpirationPeriod(TimeSpan.FromDays(30))
            .Build();
        const int referenceId = 1;

        // Act
        customer.Earned(activity, referenceId);

        // Assert
        Assert.Single(customer.PointsEarned);
        Assert.Equal(50, customer.PointBalance);
        Assert.Equal(activity.Id, customer.PointsEarned[0].ActivityId);
        Assert.NotNull(customer.PointsEarned[0].ExpirationDate);
    }

    [Fact]
    public void Earned_WhenActivityWithoutExpiration_ThenPointsAddedWithoutExpiration()
    {
        // Arrange
        var customer = _customerBuilder.Build();
        var activity = _activityBuilder
            .WithPointsReward(50)
            .WithExpirationPeriod(null)
            .Build();
        const int referenceId = 1;

        // Act
        customer.Earned(activity, referenceId);

        // Assert
        Assert.Single(customer.PointsEarned);
        Assert.Equal(50, customer.PointBalance);
        Assert.Null(customer.PointsEarned[0].ExpirationDate);
    }

    [Fact]
    public void Earned_WhenActivityIsInvalid_ThenThrowsBusinessRuleValidationException()
    {
        // Arrange
        var customer = _customerBuilder.Build();
        const int referenceId = 1;

        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => customer.Earned(null!, referenceId)
        );

        Assert.Equal("Activity type must be provided", exception.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Earned_WhenReferenceIdIsInvalid_ThenThrowsBusinessRuleValidationException(int referenceId)
    {
        // Arrange
        var customer = _customerBuilder.Build();
        var activity = _activityBuilder.Build();

        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => customer.Earned(activity, referenceId)
        );

        Assert.Equal("Reference id must be more than zero", exception.Message);
    }

    [Fact]
    public void Spent_WhenValidOffer_ThenPointsAreDeducted()
    {
        // Arrange
        var customer = _customerBuilder.Build();
        var activity = _activityBuilder
            .WithPointsReward(100)
            .Build();
        var offer = _offerBuilder
            .WithPointsCost(50)
            .Build();
        const int referenceId = 1;

        customer.Earned(activity, referenceId);
        var initialBalance = customer.PointBalance;

        // Act
        customer.Spent(offer);

        // Assert
        Assert.Single(customer.PointsSpent);
        Assert.Equal(initialBalance - offer.PointsCost, customer.PointBalance);
        Assert.Equal(offer.Id, customer.PointsSpent[0].OfferId);
    }

    [Fact]
    public void Spent_WhenOfferIsInvalid_ThenThrowsBusinessRuleValidationException()
    {
        // Arrange
        var customer = _customerBuilder.Build();

        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => customer.Spent(null!)
        );

        Assert.Equal("Offer must be provided", exception.Message);
    }

    [Fact]
    public void Spent_WhenInsufficientBalance_ThenThrowsBusinessRuleValidationException()
    {
        // Arrange
        var customer = _customerBuilder.Build();
        var activity = _activityBuilder
            .WithPointsReward(50)
            .Build();
        var offer = _offerBuilder
            .WithPointsCost(100)
            .Build();
        const int referenceId = 1;

        customer.Earned(activity, referenceId); // Balance will be 50

        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => customer.Spent(offer)
        );

        Assert.Equal("Insufficient point balance for this offer", exception.Message);
    }

    [Fact]
    public void Balance_WhenPointsExpired_ThenBalanceExcludesExpiredPoints()
    {
        // Arrange
        var customer = _customerBuilder.Build();
        var expiredActivity = _activityBuilder
            .WithPointsReward(50)
            .WithExpirationPeriod(TimeSpan.FromDays(-1))
            .Build();
        var validActivity = _activityBuilder
            .WithPointsReward(100)
            .WithExpirationPeriod(TimeSpan.FromDays(30))
            .Build();
        const int referenceId1 = 1;
        const int referenceId2 = 2;

        // Act
        customer.Earned(expiredActivity, referenceId1);
        customer.Earned(validActivity, referenceId2);

        // Assert
        Assert.Equal(2, customer.PointsEarned.Count);
        Assert.Equal(100, customer.PointBalance); // Only non-expired points should be counted
    }

    [Fact]
    public void Balance_WhenMultipleTransactions_ThenBalanceIsCalculatedCorrectly()
    {
        // Arrange
        var customer = _customerBuilder.Build();
        var activity1 = _activityBuilder.WithPointsReward(100).Build();
        var activity2 = _activityBuilder.WithPointsReward(50).Build();
        var offer = _offerBuilder.WithPointsCost(30).Build();
        const int referenceId1 = 1;
        const int referenceId2 = 2;
        
        // Act
        customer.Earned(activity1, referenceId1); // +100
        customer.Earned(activity2, referenceId2); // +50
        customer.Spent(offer); // -30

        // Assert
        Assert.Equal(activity1.PointsReward + activity2.PointsReward - offer.PointsCost, customer.PointBalance);
        Assert.Equal(2, customer.PointsEarned.Count);
        Assert.Single(customer.PointsSpent);
    }
}
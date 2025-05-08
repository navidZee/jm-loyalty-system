using JMLS.Domain;
using JMLS.Domain.Offers;

namespace JMLS.Tests.UnitTests.Domain.Offers;

public class OfferTests
{
    private readonly OfferTestDataBuilder _builder = new();

    [Fact]
    public void Constructor_WhenValidParametersProvided_ThenOfferIsCreated()
    {
        // Arrange
        const int pointSpent = 100;
        const decimal amount = 50.0m;
        const int referenceId = 1;
        var type = OfferType.Flat;
        var referenceType = OfferReferenceType.Catalog;

        // Act
        var offer = _builder
            .WithPointsCost(pointSpent)
            .WithType(type)
            .WithAmount(amount)
            .WithReferenceId(referenceId)
            .WithReferenceType(referenceType)
            .Build();

        // Assert
        Assert.NotNull(offer);
        Assert.Equal(pointSpent, offer.PointsCost);
        Assert.Equal(type, offer.Type);
        Assert.Equal(amount, offer.Amount);
        Assert.Equal(referenceId, offer.ReferenceId);
        Assert.Equal(referenceType, offer.ReferenceType);
        Assert.NotEmpty(offer.Code);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_WhenPointSpentIsNotPositive_ThenThrowsBusinessRuleValidationException(int pointSpent)
    {
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => _builder.WithPointsCost(pointSpent).Build()
        );
        
        Assert.Equal("Points cost must be greater than 0", exception.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_WhenAmountIsNotPositive_ThenThrowsBusinessRuleValidationException(int amount)
    {
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => _builder.WithAmount(amount).Build()
        );
        
        Assert.Equal("Amount must be greater than 0", exception.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_WhenReferenceIdIsInvalid_ThenThrowsBusinessRuleValidationException(int referenceId)
    {
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => _builder.WithReferenceId(referenceId).Build()
        );
        
        Assert.Equal("ReferenceId must be provided.", exception.Message);
    }

    [Fact]
    public void Constructor_WhenValidPercentageType_ThenOfferIsCreated()
    {
        // Arrange
        var type = OfferType.Percentage;
        const decimal amount = 10.0m; // 10% discount

        // Act
        var offer = _builder
            .WithType(type)
            .WithAmount(amount)
            .Build();

        // Assert
        Assert.Equal(type, offer.Type);
        Assert.Equal(amount, offer.Amount);
    }
}
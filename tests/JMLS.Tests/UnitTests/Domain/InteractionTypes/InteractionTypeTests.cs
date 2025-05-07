using JMLS.Domain;
using JMLS.Domain.InteractionTypes;

namespace JMLS.Tests.UnitTests.Domain.InteractionTypes;

public class InteractionTypeTests
{
    private readonly InteractionTypeTestDataBuilder _builder = new();

    [Fact]
    public void Constructor_WhenValidParametersProvided_ThenInteractionTypeIsCreated()
    {
        // Arrange
        const string key = "VALID_KEY";
        const string description = "Valid Description";
        const decimal amount = 50.0m;
        var expirationPeriod = TimeSpan.FromDays(30);

        // Act
        var interactionType = _builder
            .WithKey(key)
            .WithDescription(description)
            .WithAmount(amount)
            .WithExpirationPeriod(expirationPeriod)
            .Build();

        // Assert
        Assert.NotNull(interactionType);
        Assert.Equal(key, interactionType.Key);
        Assert.Equal(description, interactionType.Description);
        Assert.Equal(amount, interactionType.Amount);
        Assert.Equal(expirationPeriod, interactionType.ExpirationPeriod);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_WhenKeyIsInvalid_ThenThrowsBusinessRuleValidationException(string key)
    {
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => _builder.WithKey(key).Build()
        );
        
        Assert.Equal("Key must be provided.", exception.Message);
    }

    [Fact]
    public void Constructor_WhenKeyIsNull_ThenThrowsBusinessRuleValidationException()
    {
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => _builder.WithKey(null!).Build()
        );
        
        Assert.Equal("Key must be provided.", exception.Message);
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
    public void Constructor_WhenAmountIsNotPositive_ThenThrowsBusinessRuleValidationException(decimal amount)
    {
        // Act & Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(
            () => _builder.WithAmount(amount).Build()
        );
        
        Assert.Equal("The amount must be more than zero.", exception.Message);
    }

    [Fact]
    public void Constructor_WhenExpirationPeriodIsNull_ThenInteractionTypeIsCreatedWithoutExpiration()
    {
        // Act
        var interactionType = _builder.WithExpirationPeriod(null).Build();

        // Assert
        Assert.Null(interactionType.ExpirationPeriod);
    }
}
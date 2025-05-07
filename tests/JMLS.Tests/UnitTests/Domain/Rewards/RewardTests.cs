using JMLS.Domain;
using JMLS.Domain.Rewards;

namespace JMLS.Tests.UnitTests.Domain.Rewards;

public class RewardTests
{
    private readonly RewardTestDataBuilder _rewardTestDataBuilder = new();

    [Fact]
    public void Constructor_WhenAllParametersIsProvided_ThenAllPropertiesAreSet()
    {
        // Arrange
        const int referenceId = 200;
        const RewardReferenceType rewardReferenceType = RewardReferenceType.Discount;
        const decimal amount = 30.0m;

        // Act
        var reward = _rewardTestDataBuilder.WithReferenceId(referenceId).WithAmount(amount)
            .WithRewardReferenceType(rewardReferenceType).Build();

        // Assert
        Assert.NotNull(reward);
        Assert.Equal(referenceId, reward.RewardReferenceId);
        Assert.Equal(rewardReferenceType, reward.RewardReferenceType);
        Assert.Equal(amount, reward.Amount);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_WhenReferenceIdIsNotValid_ShouldThrowBusinessRuleValidationException(int referenceId)
    {
        // Act
        Action rewardAction = () =>  _rewardTestDataBuilder.WithReferenceId(referenceId).Build();

        // Assert
        Assert.Throws<BusinessRuleValidationException>(rewardAction);
    }    
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_WhenAmountIsNotValid_ShouldThrowBusinessRuleValidationException(decimal amount)
    {
        // Act
        Action rewardAction = () =>  _rewardTestDataBuilder.WithAmount(amount).Build();

        // Assert
        Assert.Throws<BusinessRuleValidationException>(rewardAction);
    }
}
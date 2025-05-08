namespace JMLS.Domain.Activities.Rules;

public class PointsRewardMustBeMoreThanZeroRule(decimal pointsReward) : IBusinessRule
{
    public bool IsBroken()
    {
        return pointsReward <= 0;
    }

    public string Message => "The points reward must be more than zero.";
}
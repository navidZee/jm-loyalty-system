namespace JMLS.Domain.Activities.Rules;

public class PointsEarnedMustBeMoreThanZeroRule(decimal pointsEarned) : IBusinessRule
{
    public bool IsBroken()
    {
        return pointsEarned <= 0;
    }

    public string Message => "The points earned must be more than zero.";
}
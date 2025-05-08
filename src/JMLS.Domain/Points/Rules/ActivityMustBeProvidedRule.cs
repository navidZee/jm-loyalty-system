using JMLS.Domain.Activities;

namespace JMLS.Domain.Points.Rules;

public class ActivityMustBeProvidedRule(Activity activity) : IBusinessRule
{
    public bool IsBroken()
    {
        return activity == null!;
    }

    public string Message => "Activity type must be provided";
}
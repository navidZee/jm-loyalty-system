using JMLS.Domain.Activities;

namespace JMLS.Domain.Points.Rules;

public class ActivityMustBeProvidedRule(Activity activity) : IBusinessRule
{
    public bool IsBroken()
    {
        return activity == null!;
    }

    public string Message => "Interaction type must be provided";
}
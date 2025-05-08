namespace JMLS.Domain.Activities.Rules;

public class DescriptionMustBeProvidedRule(string description) : IBusinessRule
{
    public bool IsBroken()
    {
        return string.IsNullOrWhiteSpace(description);
    }

    public string Message => "Description must be provided.";
}
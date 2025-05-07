namespace JMLS.Domain.InteractionTypes.Rules;

public class DescriptionMustBeProvidedRule(string key) : IBusinessRule
{
    public bool IsBroken()
    {
        return string.IsNullOrWhiteSpace(key);
    }

    public string Message => "Description must be provided.";
}
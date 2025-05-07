namespace JMLS.Domain.InteractionTypes.Rules;

public class KeyMustBeProvidedRule(string key) : IBusinessRule
{
    public bool IsBroken()
    {
        return string.IsNullOrWhiteSpace(key);
    }

    public string Message => "Key must be provided.";
}
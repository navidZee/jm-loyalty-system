namespace JMLS.Domain.Activities.Rules;

public class TitleMustBeProvidedRule(string title) : IBusinessRule
{
    public bool IsBroken()
    {
        return string.IsNullOrWhiteSpace(title);
    }

    public string Message => "Title must be provided.";
}
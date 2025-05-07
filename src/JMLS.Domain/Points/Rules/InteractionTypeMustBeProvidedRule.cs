using JMLS.Domain.InteractionTypes;

namespace JMLS.Domain.Points.Rules;

public class InteractionTypeMustBeProvidedRule(InteractionType interactionType) : IBusinessRule
{
    public bool IsBroken()
    {
        return interactionType is { Id: <= 0 };
    }

    public string Message => "Interaction type must be provided";
}
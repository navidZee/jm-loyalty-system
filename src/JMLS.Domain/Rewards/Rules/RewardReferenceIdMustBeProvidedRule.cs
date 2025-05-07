namespace JMLS.Domain.Rewards.Rules;

public class RewardReferenceIdMustBeProvidedRule(int referenceId) : IBusinessRule
{
    public bool IsBroken()
    {
        return referenceId <= 0;
    }

    public string Message => "ReferenceId must be provided.";
}
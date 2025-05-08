namespace JMLS.Domain.Offers.Rules;

public class OfferReferenceIdMustBeProvidedRule(int referenceId) : IBusinessRule
{
    public bool IsBroken()
    {
        return referenceId <= 0;
    }

    public string Message => "ReferenceId must be provided.";
}
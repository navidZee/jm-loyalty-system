using FluentValidation;
using JMLS.RestAPI.Requests;

namespace JMLS.RestAPI.Validators;

public class RequestToSpentValidator : AbstractValidator<RequestToSpentDto>
{
    public RequestToSpentValidator()
    {
        _ = this.RuleFor(r => r.OfferId).GreaterThan(0).WithMessage("offer id cannot be 0");
    }
}
using FluentValidation;
using JMLS.RestAPI.Requests;

namespace JMLS.RestAPI.Validators;

public class RequestToEarnValidator : AbstractValidator<RequestToEarnDto>
{
    public RequestToEarnValidator()
    {
        _ = this.RuleFor(r => r.ActivityId).GreaterThan(0).WithMessage("activity id cannot be 0");
        _ = this.RuleFor(r => r.ReferenceId).GreaterThan(0).WithMessage("activity reference id cannot be 0");
    }
}
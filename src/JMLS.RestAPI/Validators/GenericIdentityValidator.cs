using FluentValidation;

namespace JMLS.RestAPI.Validators;

public class GenericIdentityValidator : AbstractValidator<Guid>
{
    public GenericIdentityValidator()
    {
        _ = this.RuleFor(r => r)
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("The Id supplied in the request is not valid.");
    }
}

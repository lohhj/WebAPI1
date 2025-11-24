using FluentValidation;
using WebAPI1.Application.Commands;

namespace WebAPI1.Application.Validators;

public class FreelancerCommandBaseValidator<T> : AbstractValidator<T> where T : FreelancerCommandBase
{
    public FreelancerCommandBaseValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .MinimumLength(8).WithMessage("Phone number must be at least 8 digits.");
    }
}
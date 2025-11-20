using FluentValidation;
using WebAPI1.Application.Commands;

namespace WebAPI1.Application.Validators;

public class UpdateFreelancerCommandValidator : AbstractValidator<UpdateFreelancerCommand>
{
    public UpdateFreelancerCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("A valid email is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .MinimumLength(8)
            .WithMessage("Phone number must be at least 8 digits.");
    }
}
using FluentValidation;
using WebAPI1.Application.Commands;

namespace WebAPI1.Application.Validators;

public class UpdateFreelancerCommandValidator : FreelancerCommandBaseValidator<UpdateFreelancerCommand>
{
    public UpdateFreelancerCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Invalid ID.");
    }
}
using FluentValidation;
using WebAPI1.Application.Commands;

namespace WebAPI1.Application.Validators
{
    public class ArchiveFreelancerCommandValidator : AbstractValidator<ArchiveFreelancerCommand>
    {
        public ArchiveFreelancerCommandValidator()
        {
            RuleFor(x => x.Archived)
                .NotNull().WithMessage("Archive status is required.");
        }
    }
}
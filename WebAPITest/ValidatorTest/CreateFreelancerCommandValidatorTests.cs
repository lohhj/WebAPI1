using FluentValidation;
using WebAPI1.Application.Commands;
using WebAPI1.Application.Validators;
using Xunit;

namespace Application.UnitTests.Validators;

public class CreateFreelancerCommandValidatorTests
    : FreelancerCommandBaseValidatorTests<CreateFreelancerCommand>
{
    protected override AbstractValidator<CreateFreelancerCommand> Validator =>
        new CreateFreelancerCommandValidator();
}
using FluentValidation;
using FluentValidation.TestHelper;
using WebAPI1.Application.Commands;
using WebAPI1.Application.Validators;
using Xunit;

namespace Application.UnitTests.Validators;

public class UpdateFreelancerCommandValidatorTests : FreelancerCommandBaseValidatorTests<UpdateFreelancerCommand>
{
    protected override AbstractValidator<UpdateFreelancerCommand> Validator => new UpdateFreelancerCommandValidator();

}
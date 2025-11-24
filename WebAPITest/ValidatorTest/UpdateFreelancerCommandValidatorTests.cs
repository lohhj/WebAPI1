using FluentValidation;
using FluentValidation.TestHelper;
using WebAPI1.Application.Commands;
using WebAPI1.Application.Validators;
using Xunit;

namespace Application.UnitTests.Validators;

public class UpdateFreelancerCommandValidatorTests
    : FreelancerCommandBaseValidatorTests<UpdateFreelancerCommand>
{
    protected override AbstractValidator<UpdateFreelancerCommand> Validator =>
        new UpdateFreelancerCommandValidator();

    [Fact]
    public void ShouldHaveError_WhenIdIsInvalid()
    {
        // Arrange
        var command = new UpdateFreelancerCommand { Id = 0 };

        // Act
        var result = Validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
              .WithErrorMessage("Invalid ID.");
    }
}
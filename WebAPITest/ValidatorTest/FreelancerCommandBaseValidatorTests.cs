using FluentValidation;
using FluentValidation.TestHelper;
using WebAPI1.Application.Commands;
using Xunit;

namespace Application.UnitTests.Validators;

public abstract class FreelancerCommandBaseValidatorTests<TCommand>
    where TCommand : FreelancerCommandBase, new()
{
    protected abstract AbstractValidator<TCommand> Validator { get; }

    [Fact]
    public void ShouldHaveError_WhenUsernameIsEmpty()
    {
        var command = new TCommand { Username = "" };
        var result = Validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Username).WithErrorMessage("Username is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenEmailIsInvalid()
    {
        var command = new TCommand { Email = "invalid-email" };
        var result = Validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage("A valid email is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenPhoneNumberIsTooShort()
    {
        var command = new TCommand { PhoneNumber = "123" };
        var result = Validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber).WithErrorMessage("Phone number must be at least 8 digits.");
    }
}
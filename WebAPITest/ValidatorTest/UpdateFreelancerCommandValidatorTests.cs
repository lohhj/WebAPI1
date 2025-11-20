using Xunit;
using FluentValidation.TestHelper;
using WebAPI1.Application.Commands;
using WebAPI1.Application.Validators;

namespace Application.UnitTests.Validators
{
    public class UpdateFreelancerCommandValidatorTests
    {
        private readonly UpdateFreelancerCommandValidator _validator;

        public UpdateFreelancerCommandValidatorTests()
        {
            _validator = new UpdateFreelancerCommandValidator();
        }

        [Fact]
        public void ShouldHaveError_WhenUsernameIsEmpty()
        {
            var command = new UpdateFreelancerCommand { Username = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Username).WithErrorMessage("Username is required.");
        }

        [Fact]
        public void ShouldHaveError_WhenEmailIsInvalid()
        {
            var command = new UpdateFreelancerCommand { Username = "Test", Email = "not-email" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage("A valid email is required.");
        }

        [Fact]
        public void ShouldHaveError_WhenPhoneNumberIsTooShort()
        {
            var command = new UpdateFreelancerCommand { Username = "Test", Email = "test@gmail.com", PhoneNumber = "123" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber).WithErrorMessage("Phone number must be at least 8 digits.");
        }

        [Fact]
        public void ShouldNotHaveError_WhenRequestIsValid()
        {
            var command = new UpdateFreelancerCommand
            {
                Username = "Test User",
                Email = "test@gmail.com",
                PhoneNumber = "12345678"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
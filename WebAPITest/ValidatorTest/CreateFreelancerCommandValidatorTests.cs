using Xunit;
using FluentValidation.TestHelper;
using WebAPI1.Application.Commands;
using WebAPI1.Application.Validators;

namespace Application.UnitTests.Validators
{
    public class CreateFreelancerCommandValidatorTests
    {
        private readonly CreateFreelancerCommandValidator _validator;

        public CreateFreelancerCommandValidatorTests()
        {
            _validator = new CreateFreelancerCommandValidator();
        }

        [Fact]
        public void ShouldHaveError_WhenUsernameIsEmpty()
        {
            // Arrange
            var command = new CreateFreelancerCommand
            {
                Username = "",
                Email = "test@gmail.com",
                PhoneNumber = "123"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Username);
        }

        [Fact]
        public void ShouldHaveError_WhenEmailIsInvalid()
        {
            // Arrange
            var command = new CreateFreelancerCommand
            {
                Username = "Test",
                Email = "not-a-valid-email",
                PhoneNumber = "123"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("A valid email is required.");
        }

        [Fact]
        public void ShouldHaveError_WhenPhoneNumberIsInvalid()
        {
            // Arrange
            var command = new CreateFreelancerCommand
            {
                Username = "Test",
                Email = "test@gmail.com",
                PhoneNumber = "123"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                  .WithErrorMessage("Phone number must be at least 8 digits.");
        }

        [Fact]
        public void ShouldNotHaveError_WhenRequestIsValid()
        {
            // Arrange
            var command = new CreateFreelancerCommand
            {
                Username = "Test User",
                Email = "test@gmail.com",
                PhoneNumber = "12345678"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
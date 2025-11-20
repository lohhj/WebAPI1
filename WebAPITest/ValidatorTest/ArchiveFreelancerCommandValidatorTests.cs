using Xunit;
using FluentValidation.TestHelper;
using WebAPI1.Application.Commands;
using WebAPI1.Application.Validators;

namespace Application.UnitTests.Validators
{
    public class ArchiveFreelancerCommandValidatorTests
    {
        private readonly ArchiveFreelancerCommandValidator _validator;

        public ArchiveFreelancerCommandValidatorTests()
        {
            _validator = new ArchiveFreelancerCommandValidator();
        }

        [Fact]
        public void ShouldNotHaveError_WhenArchivedIsTrue()
        {
            var command = new ArchiveFreelancerCommand { Archived = true };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ShouldNotHaveError_WhenArchivedIsFalse()
        {
            var command = new ArchiveFreelancerCommand { Archived = false };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
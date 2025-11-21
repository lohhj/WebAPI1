using Xunit;
using Moq;
using WebAPI1.Domain.Interfaces;
using Domain.Entities;
using WebAPI1.Application.Commands;

namespace Application.UnitTests
{
    public class UpdateFreelancerCommandHandlerTests
    {
        private readonly Mock<IFreelancerRepository> _mockRepository;
        private readonly UpdateFreelancerCommandHandler _handler;

        public UpdateFreelancerCommandHandlerTests()
        {
            _mockRepository = new Mock<IFreelancerRepository>();
            _handler = new UpdateFreelancerCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenUpdateSucceeds()
        {
            // Arrange
            var command = new UpdateFreelancerCommand
            {
                Id = 1,
                Username = "Updated User",
                Email = "updated@gmail.com",
                PhoneNumber = "87654321",
                Archived = true,
                Skillsets = new List<string> { "Climb" },
                Hobbies = new List<string> { "Climbing" }
            };
            _mockRepository.Setup(repo => repo.UpdateAsync(command.Id, It.IsAny<Freelancer>())).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _mockRepository.Verify(
                repo => repo.UpdateAsync(1, It.Is<Freelancer>(f =>
                    f.Id == 1 &&
                    f.Username == "Updated User" &&
                    f.Email == "updated@gmail.com" &&
                    f.PhoneNumber == "87654321" &&
                    f.Archived == true &&
                    f.Skillsets.Count == 1 &&
                    f.Skillsets[0].Skill == "Climb"&&
                    f.Hobbies.Count == 1 &&
                    f.Hobbies[0].Hobby == "Climbing"
                )),
                Times.Once
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenUpdateFails()
        {
            // Arrange
            var command = new UpdateFreelancerCommand { Id = 99 }; 
            _mockRepository.Setup(repo => repo.UpdateAsync(command.Id, It.IsAny<Freelancer>())).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("not found", result.Errors[0].Message);
        }
    }
}
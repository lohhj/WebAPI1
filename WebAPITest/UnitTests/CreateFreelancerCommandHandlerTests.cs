using Xunit;
using Moq;
using WebAPI1.Domain.Interfaces;
using Domain.Entities;
using WebAPI1.Application.Commands;

namespace Application.UnitTests
{
    public class CreateFreelancerCommandHandlerTests
    {
        private readonly Mock<IFreelancerRepository> _mockRepository;
        private readonly CreateFreelancerCommandHandler _handler;

        public CreateFreelancerCommandHandlerTests()
        {
            _mockRepository = new Mock<IFreelancerRepository>();
            _handler = new CreateFreelancerCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallRepository_WithCorrectFreelancerEntity()
        {
            // Arrange
            var command = new CreateFreelancerCommand
            {
                Username = "New User",
                Email = "new@gmail.com",
                PhoneNumber = "12345678",
                Archived = false,
                Skillsets = new List<string> { "C#", "SQL" },
                Hobbies = new List<string> { "Testing" }
            };
            var expectedNewId = 10;
            _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Freelancer>())).ReturnsAsync(expectedNewId);

            // Act
            var actualId = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedNewId, actualId);
            _mockRepository.Verify(
                repo => repo.CreateAsync(It.Is<Freelancer>(f =>
                    f.Username == "New User" &&
                    f.Email == "new@gmail.com" &&
                    f.PhoneNumber == "12345678" &&
                    f.Archived == false &&
                    f.Skillsets.Count == 2 &&
                    f.Skillsets[0].Skill == "C#" &&
                    f.Skillsets[1].Skill == "SQL" &&
                    f.Hobbies.Count == 1 &&
                    f.Hobbies[0].Hobby == "Testing"
                )),
                Times.Once
            );
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var command = new CreateFreelancerCommand { Username = "Test", Email = "a@a.com", PhoneNumber = "111" };
            var dbException = new InvalidOperationException("Database error");
            _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Freelancer>())).ThrowsAsync(dbException);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _handler.Handle(command, CancellationToken.None)
            );
            Assert.Equal("Database error", ex.Message);
        }
    }
}
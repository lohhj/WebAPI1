using Xunit;
using Moq;
using WebAPI1.Domain.Interfaces;
using WebAPI1.Application.Commands;

namespace Application.UnitTests
{
    public class DeleteFreelancerCommandHandlerTests
    {
        private readonly Mock<IFreelancerRepository> _mockRepository;
        private readonly DeleteFreelancerCommandHandler _handler;

        public DeleteFreelancerCommandHandlerTests()
        {
            _mockRepository = new Mock<IFreelancerRepository>();
            _handler = new DeleteFreelancerCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenDeleteSucceeds()
        {
            // Arrange
            var command = new DeleteFreelancerCommand { Id = 1 };
            _mockRepository.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenDeleteFails()
        {
            // Arrange
            var command = new DeleteFreelancerCommand { Id = 99 }; 
            _mockRepository.Setup(repo => repo.DeleteAsync(99)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}
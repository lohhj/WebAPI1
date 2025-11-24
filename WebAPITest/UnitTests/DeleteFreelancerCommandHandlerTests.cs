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
        public async Task Handle_ShouldReturnOk_WhenDeleteSucceeds()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeleteFreelancerCommand { Id = id };
            _mockRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenDeleteFails()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeleteFreelancerCommand { Id = id };
            _mockRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
        }
    }
}
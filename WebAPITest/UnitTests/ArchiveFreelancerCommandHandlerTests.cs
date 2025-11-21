using Xunit;
using Moq;
using WebAPI1.Domain.Interfaces;
using WebAPI1.Application.Commands;

namespace Application.UnitTests
{
    public class ArchiveFreelancerCommandHandlerTests
    {
        private readonly Mock<IFreelancerRepository> _mockRepository;
        private readonly ArchiveFreelancerCommandHandler _handler;

        public ArchiveFreelancerCommandHandlerTests()
        {
            _mockRepository = new Mock<IFreelancerRepository>();
            _handler = new ArchiveFreelancerCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenArchiveSucceeds()
        {
            // Arrange
            var command = new ArchiveFreelancerCommand { Id = 1, Archived = true };
            _mockRepository.Setup(repo => repo.ArchiveAsync(1, true)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _mockRepository.Verify(repo => repo.ArchiveAsync(1, true), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenUnArchiveSucceeds()
        {
            // Arrange
            var command = new ArchiveFreelancerCommand { Id = 1, Archived = false };
            _mockRepository.Setup(repo => repo.ArchiveAsync(1, false)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _mockRepository.Verify(repo => repo.ArchiveAsync(1, false), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenArchiveFails()
        {
            // Arrange
            var command = new ArchiveFreelancerCommand { Id = 99, Archived = true };
            _mockRepository.Setup(repo => repo.ArchiveAsync(99, true)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
        }
    }
}
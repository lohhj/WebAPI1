using Xunit;
using Moq;
using WebAPI1.Domain.Interfaces;
using WebAPI1.Application.Commands;

namespace Application.UnitTests;

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
    public async Task Handle_ShouldReturnOk_WhenArchiveSucceeds()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new ArchiveFreelancerCommand { Id = id, Archived = true };

        // 模拟成功
        _mockRepository.Setup(repo => repo.ArchiveAsync(id, true)).ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_ShouldReturnFail_WhenFreelancerNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new ArchiveFreelancerCommand { Id = id, Archived = true };

        _mockRepository.Setup(repo => repo.ArchiveAsync(id, true)).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Contains("not found", result.Errors[0].Message);
    }
}
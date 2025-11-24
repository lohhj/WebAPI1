using Xunit;
using Moq;
using WebAPI1.Domain.Interfaces;
using Domain.Entities;
using WebAPI1.Application.Commands;

namespace Application.UnitTests;

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
    public async Task Handle_ShouldReturnOk_WhenUpdateSucceeds()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new UpdateFreelancerCommand { Id = id, Username = "Updated" };
        _mockRepository.Setup(repo => repo.UpdateAsync(id, It.IsAny<Freelancer>())).ReturnsAsync(true);

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
        var command = new UpdateFreelancerCommand { Id = id };
        _mockRepository.Setup(repo => repo.UpdateAsync(id, It.IsAny<Freelancer>())).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Contains("not found", result.Errors[0].Message);
    }
}
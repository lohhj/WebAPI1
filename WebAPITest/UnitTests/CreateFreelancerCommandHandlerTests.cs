using Xunit;
using Moq;
using WebAPI1.Domain.Interfaces;
using Domain.Entities;
using WebAPI1.Application.Commands;

namespace Application.UnitTests;

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
    public async Task Handle_ShouldReturnSuccess_WithId()
    {
        // Arrange
        var command = new CreateFreelancerCommand
        {
            Username = "New User",
            Email = "new@email.com",
            PhoneNumber = "12345678",
            Archived = false,
            Skillsets = new List<string> { "C#" },
            Hobbies = new List<string> { "Testing" }
        };
        var expectedNewId = 10;
        _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Freelancer>())).ReturnsAsync(expectedNewId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedNewId, result.Value);
    }

    [Fact]
    public async Task Handle_ShouldReturnFail_WhenExceptionOccurs()
    {
        // Arrange
        var command = new CreateFreelancerCommand
        {
            Username = "Test",
            Skillsets = new List<string>(),
            Hobbies = new List<string>()
        };
        _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Freelancer>())).ThrowsAsync(new Exception("DB Error"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Contains("Creation failed", result.Errors[0].Message);
        Assert.Equal("DB Error", result.Errors[0].Reasons[0].Message);
    }
}
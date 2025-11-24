using Xunit;
using Moq;
using WebAPI1.Domain.Interfaces;
using Domain.Entities;
using WebAPI1.Application.Queries;

namespace Application.UnitTests;

public class GetAllFreelancersQueryHandlerTests
{
    private readonly Mock<IFreelancerRepository> _mockRepository;
    private readonly GetAllFreelancersQueryHandler _handler;

    public GetAllFreelancersQueryHandlerTests()
    {
        _mockRepository = new Mock<IFreelancerRepository>();
        _handler = new GetAllFreelancersQueryHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldCallGetAll_WhenKeywordIsHasValue()
    {
        // Arrange
        var freelancers = new List<Freelancer>
        {
            new Freelancer { Id = Guid.NewGuid(), Username = "User1", Email="user1@gmail.com", PhoneNumber="11111111", Archived=false, Skillsets = new(), Hobbies = new() }
        };

        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(freelancers);

        // Act
        var query = new GetAllFreelancersQuery { Keyword = null };
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value.Count());

        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockRepository.Verify(r => r.SearchAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldCallSearch_WhenKeywordIsProvided()
    {
        // Arrange
        var keyword = "User1";
        var freelancers = new List<Freelancer>
        {
            new Freelancer { Id = Guid.NewGuid(), Username = "User1", Email="user1@gmail.com", PhoneNumber="11111111", Archived=false, Skillsets = new(), Hobbies = new() }
        };

        _mockRepository.Setup(r => r.SearchAsync(keyword)).ReturnsAsync(freelancers);

        // Act
        var query = new GetAllFreelancersQuery { Keyword = keyword };
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value.Count());

        _mockRepository.Verify(r => r.SearchAsync(keyword), Times.Once);
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoDataFound()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Freelancer>());

        // Act
        var result = await _handler.Handle(new GetAllFreelancersQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}
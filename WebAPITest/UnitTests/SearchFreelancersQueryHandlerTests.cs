using Xunit;
using Moq;
using WebAPI1.Domain.Interfaces;
using Domain.Entities;
using WebAPI1.Domain.Entities;
using WebAPI1.Application.Queries;

namespace Application.UnitTests
{
    public class SearchFreelancersQueryHandlerTests
    {
        private readonly Mock<IFreelancerRepository> _mockRepository;
        private readonly SearchFreelancersQueryHandler _handler;

        public SearchFreelancersQueryHandlerTests()
        {
            _mockRepository = new Mock<IFreelancerRepository>();
            _handler = new SearchFreelancersQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedDtos_WhenMatchesFound()
        {
            // Arrange
            var keyword = "test";
            var freelancers = new List<Freelancer>
            {
                new Freelancer { Id = 1, Username = "test_user", Email = "test@gmail.com", PhoneNumber = "11111111", Archived = false, Skillsets = new List<Skillset> { new Skillset { Skill = "SQL" } }, Hobbies = new List<Hobbies>() }
            };
            _mockRepository.Setup(r => r.SearchAsync(keyword)).ReturnsAsync(freelancers);

            // Act
            var result = await _handler.Handle(new SearchFreelancersQuery { Keyword = keyword }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("test_user", result.First().Username);
            Assert.Equal("SQL", result.First().Skillsets.First());
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoMatchesFound()
        {
            // Arrange
            var keyword = "nomatch";
            _mockRepository.Setup(r => r.SearchAsync(keyword)).ReturnsAsync(new List<Freelancer>());

            // Act
            var result = await _handler.Handle(new SearchFreelancersQuery { Keyword = keyword }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
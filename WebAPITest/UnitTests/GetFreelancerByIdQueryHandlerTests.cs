using Xunit;
using Moq;
using WebAPI1.Domain.Interfaces;
using Domain.Entities;
using WebAPI1.Domain.Entities;
using WebAPI1.Application.Queries;

namespace Application.UnitTests
{
    public class GetFreelancerByIdQueryHandlerTests
    {
        private readonly Mock<IFreelancerRepository> _mockRepository;
        private readonly GetFreelancerByIdQueryHandler _handler;

        public GetFreelancerByIdQueryHandlerTests()
        {
            _mockRepository = new Mock<IFreelancerRepository>();
            _handler = new GetFreelancerByIdQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnDto_WhenFreelancerExists()
        {
            // Arrange
            var fakeFreelancer = new Freelancer
            {
                Id = 1,
                Username = "Test User",
                Email = "test@gmail.com",
                PhoneNumber = "12345678",
                Archived = false,
                Skillsets = new List<Skillset> { new Skillset { Skill = "C#" } },
                Hobbies = new List<Hobbies> { new Hobbies { Hobby = "Coding" } }
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(fakeFreelancer);

            // Act
            var query = new GetFreelancerByIdQuery { Id = 1 };
            var resultDto = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultDto);
            Assert.Equal(1, resultDto.Id);
            Assert.Equal("Test User", resultDto.Username);
            Assert.Equal("test@gmail.com", resultDto.Email);
            Assert.Single(resultDto.Skillsets);
            Assert.Equal("C#", resultDto.Skillsets[0]);
            Assert.Equal("Coding", resultDto.Hobbies[0]);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenFreelancerDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Freelancer)null);

            // Act
            var query = new GetFreelancerByIdQuery { Id = 1 };
            var resultDto = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(resultDto);
        }
    }
}
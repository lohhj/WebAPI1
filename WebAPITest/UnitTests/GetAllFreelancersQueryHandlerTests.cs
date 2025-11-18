using Xunit;
using Moq;
using WebAPI1.Domain.Interfaces;
using Domain.Entities;
using WebAPI1.Domain.Entities;
using WebAPI1.Application.Queries;

namespace Application.UnitTests
{
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
        public async Task Handle_ShouldReturnMappedDtos_WhenFreelancersExist()
        {
            // Arrange
            var freelancers = new List<Freelancer>
            {
                new Freelancer { Id = 1, Username = "User1", Email = "user1@gmail.com", PhoneNumber = "11111111", Archived = false, Skillsets = new List<Skillset> { new Skillset { Skill = "C#" } }, Hobbies = new List<Hobbies>() },
                new Freelancer { Id = 2, Username = "User2", Email = "user2@gmail.com", PhoneNumber = "22222222", Archived = false, Skillsets = new List<Skillset>(), Hobbies = new List<Hobbies> { new Hobbies { Hobby = "Art" } } }
            };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(freelancers);

            // Act
            var result = await _handler.Handle(new GetAllFreelancersQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("User1", result.First().Username);
            Assert.Equal("C#", result.First().Skillsets.First());
            Assert.Equal("Art", result.Last().Hobbies.First());
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoFreelancersExist()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Freelancer>());

            // Act
            var result = await _handler.Handle(new GetAllFreelancersQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
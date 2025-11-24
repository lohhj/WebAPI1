using Domain.Entities;
using FluentResults;
using Moq;
using System.Web.Http.Results;
using WebAPI1.Application.Queries;
using WebAPI1.Domain.Entities;
using WebAPI1.Domain.Interfaces;
using Xunit;

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
        public async Task Handle_ShouldReturnSuccess_WhenFreelancerExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var fakeFreelancer = new Freelancer
            {
                Id = id,
                Username = "Test User",
                Email = "test@email.com",
                PhoneNumber = "12345678",
                Archived = false,
                Skillsets = new List<Skillset> { new Skillset { Skill = "C#" } },
                Hobbies = new List<Hobbies> { new Hobbies { Hobby = "Coding" } }
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(fakeFreelancer);

            // Act
            var query = new GetFreelancerByIdQuery { Id = id };
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(id, result.Value.Id);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenFreelancerDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Freelancer)null);

            // Act
            var query = new GetFreelancerByIdQuery { Id = id };
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("not found", result.Errors[0].Message);
        }
    }
}
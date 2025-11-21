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
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            var dto = result.Value;
            Assert.NotNull(dto);
            Assert.Equal(1, dto.Id);
            Assert.Equal("Test User", dto.Username);
            Assert.Equal("test@gmail.com", dto.Email);
            Assert.Single(dto.Skillsets);
            Assert.Equal("C#", dto.Skillsets[0]);
            Assert.Equal("Coding", dto.Hobbies[0]);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenFreelancerDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Freelancer)null);

            // Act
            var query = new GetFreelancerByIdQuery { Id = 1 };
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("not found", result.Errors[0].Message);
        }
    }
}
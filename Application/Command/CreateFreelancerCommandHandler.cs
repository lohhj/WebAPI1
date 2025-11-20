using Domain.Entities;
using MediatR;
using WebAPI1.Domain.Entities;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Commands
{
    public class CreateFreelancerCommandHandler(IFreelancerRepository repository) : IRequestHandler<CreateFreelancerCommand, int>
    {
        public async Task<int> Handle(CreateFreelancerCommand request, CancellationToken cancellationToken)
        {
            var freelancer = new Freelancer
            {
                Username = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Archived = request.Archived,
                Skillsets = request.Skillsets.Select(skill => new Skillset { Skill = skill }).ToList(),
                Hobbies = request.Hobbies.Select(hobby => new Hobbies { Hobby = hobby }).ToList()
            };

            return await repository.CreateAsync(freelancer);
        }
    }
}
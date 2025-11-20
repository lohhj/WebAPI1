using Domain.Entities;
using MediatR;
using WebAPI1.Domain.Entities;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Commands
{
    public class UpdateFreelancerCommandHandler(IFreelancerRepository repository) : IRequestHandler<UpdateFreelancerCommand, bool>
    {
        public async Task<bool> Handle(UpdateFreelancerCommand request, CancellationToken cancellationToken)
        {
            var freelancer = new Freelancer
            {
                Id = request.Id,
                Username = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Archived = request.Archived,
                Skillsets = request.Skillsets.Select(skill => new Skillset { Skill = skill }).ToList(),
                Hobbies = request.Hobbies.Select(hobby => new Hobbies { Hobby = hobby }).ToList()
            };

            return await repository.UpdateAsync(request.Id, freelancer);
        }
    }
}
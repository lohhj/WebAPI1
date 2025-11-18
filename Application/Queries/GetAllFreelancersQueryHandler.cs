using MediatR;
using WebAPI1.Application.DTOs;
using WebAPI1.Domain.Entities;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Queries
{
    public class GetAllFreelancersQueryHandler : IRequestHandler<GetAllFreelancersQuery, IEnumerable<CreateFreelancerResponse>>
    {
        private readonly IFreelancerRepository _repository;

        public GetAllFreelancersQueryHandler(IFreelancerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CreateFreelancerResponse>> Handle(GetAllFreelancersQuery request, CancellationToken cancellationToken)
        {
            var freelancers = await _repository.GetAllAsync();

            return freelancers.Select(f => new CreateFreelancerResponse
            {
                Id = f.Id,
                Username = f.Username,
                Email = f.Email,
                PhoneNumber = f.PhoneNumber,
                Archived = f.Archived,
                Skillsets = f.Skillsets.Select(s => s.Skill).ToList(),
                Hobbies = f.Hobbies.Select(h => h.Hobby).ToList()
            });
        }
    }
}
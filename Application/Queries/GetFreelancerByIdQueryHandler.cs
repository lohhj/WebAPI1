using MediatR;
using Microsoft.SharePoint.Client;
using WebAPI1.Application.DTOs;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Queries;

public class GetFreelancerByIdQueryHandler(IFreelancerRepository repository) : IRequestHandler<GetFreelancerByIdQuery, CreateFreelancerResponse?>
{
    public async Task<CreateFreelancerResponse?> Handle(GetFreelancerByIdQuery request, CancellationToken cancellationToken)
    {
        var freelancer = await repository.GetByIdAsync(request.Id);
        if (freelancer == null) return null;

        return new CreateFreelancerResponse
        {
            Id = freelancer.Id,
            Username = freelancer.Username,
            Email = freelancer.Email,
            PhoneNumber = freelancer.PhoneNumber,
            Archived = freelancer.Archived,
            Skillsets = freelancer.Skillsets.Select(s => s.Skill).ToList(),
            Hobbies = freelancer.Hobbies.Select(h => h.Hobby).ToList()
        };
    }
}

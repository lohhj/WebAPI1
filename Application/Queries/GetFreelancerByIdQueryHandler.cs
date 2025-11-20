using FluentResults;
using MediatR;
using Microsoft.SharePoint.Client;
using WebAPI1.Application.DTOs;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Queries;

public class GetFreelancerByIdQueryHandler(IFreelancerRepository repository)
        : IRequestHandler<GetFreelancerByIdQuery, Result<CreateFreelancerResponse>>
{
    public async Task<Result<CreateFreelancerResponse>> Handle(GetFreelancerByIdQuery request, CancellationToken cancellationToken)
    {
        var freelancer = await repository.GetByIdAsync(request.Id);
        if (freelancer == null)
        {
            return Result.Fail($"Freelancer with ID {request.Id} not found");
        }
        var dto = new CreateFreelancerResponse
        {
            Id = freelancer.Id,
            Username = freelancer.Username,
            Email = freelancer.Email,
            PhoneNumber = freelancer.PhoneNumber,
            Archived = freelancer.Archived,
            Skillsets = freelancer.Skillsets.Select(s => s.Skill).ToList(),
            Hobbies = freelancer.Hobbies.Select(h => h.Hobby).ToList()
        };
        return Result.Ok(dto);
    }
}

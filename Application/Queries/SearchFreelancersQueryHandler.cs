using FluentResults;
using MediatR;
using WebAPI1.Application.DTOs;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Queries;

public class SearchFreelancersQueryHandler(IFreelancerRepository repository)
        : IRequestHandler<SearchFreelancersQuery, Result<IEnumerable<CreateFreelancerResponse>>>
{
    public async Task<Result<IEnumerable<CreateFreelancerResponse>>> Handle(SearchFreelancersQuery request, CancellationToken cancellationToken)
    {
        var freelancers = await repository.SearchAsync(request.Keyword);

        var dtos = freelancers.Select(f => new CreateFreelancerResponse
        {
            Id = f.Id,
            Username = f.Username,
            Email = f.Email,
            PhoneNumber = f.PhoneNumber,
            Archived = f.Archived,
            Skillsets = f.Skillsets.Select(s => s.Skill).ToList(),
            Hobbies = f.Hobbies.Select(h => h.Hobby).ToList()
        });
        return Result.Ok(dtos);
    }
}
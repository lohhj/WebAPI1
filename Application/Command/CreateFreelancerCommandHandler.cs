using Domain.Entities;
using FluentResults;
using MediatR;
using WebAPI1.Domain.Entities;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Commands;

public class CreateFreelancerCommandHandler(IFreelancerRepository repository) : IRequestHandler<CreateFreelancerCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateFreelancerCommand request, CancellationToken cancellationToken)
    {
        try
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

            var id = await repository.CreateAsync(freelancer);

            return Result.Ok(id);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Creation failed").CausedBy(ex));
        }
    }
}
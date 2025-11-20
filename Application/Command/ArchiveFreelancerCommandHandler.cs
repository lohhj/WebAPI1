using FluentResults;
using MediatR;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Commands;

public class ArchiveFreelancerCommandHandler(IFreelancerRepository repository) : IRequestHandler<ArchiveFreelancerCommand, Result>
{
    public async Task<Result> Handle(ArchiveFreelancerCommand request, CancellationToken cancellationToken)
    {
        var success = await repository.ArchiveAsync(request.Id, request.Archived);

        if (!success)
        {
            return Result.Fail($"Freelancer with ID {request.Id} not found");
        }

        return Result.Ok();
    }
}
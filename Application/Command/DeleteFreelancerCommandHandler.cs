using FluentResults;
using MediatR;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Commands
{
    public class DeleteFreelancerCommandHandler(IFreelancerRepository repository) : IRequestHandler<DeleteFreelancerCommand, Result>
    {
        public async Task<Result> Handle(DeleteFreelancerCommand request, CancellationToken cancellationToken)
        {
            var success = await repository.DeleteAsync(request.Id);

            if (!success)
            {
                return Result.Fail($"Freelancer with ID {request.Id} not found");
            }

            return Result.Ok();
        }
    }
}
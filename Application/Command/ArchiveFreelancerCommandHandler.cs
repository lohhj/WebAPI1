using MediatR;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Commands
{
    public class ArchiveFreelancerCommandHandler(IFreelancerRepository repository) : IRequestHandler<ArchiveFreelancerCommand, bool>
    {
        public async Task<bool> Handle(ArchiveFreelancerCommand request, CancellationToken cancellationToken)
        {
            return await repository.ArchiveAsync(request.Id, request.Archived);
        }
    }
}
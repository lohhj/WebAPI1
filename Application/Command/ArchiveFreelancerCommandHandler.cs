using MediatR;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Commands
{
    public class ArchiveFreelancerCommandHandler : IRequestHandler<ArchiveFreelancerCommand, bool>
    {
        private readonly IFreelancerRepository _repository;

        public ArchiveFreelancerCommandHandler(IFreelancerRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(ArchiveFreelancerCommand request, CancellationToken cancellationToken)
        {
            return await _repository.ArchiveAsync(request.Id, request.Archived);
        }
    }
}
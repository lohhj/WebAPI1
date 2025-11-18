using MediatR;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Commands
{
    public class DeleteFreelancerCommandHandler : IRequestHandler<DeleteFreelancerCommand, bool>
    {
        private readonly IFreelancerRepository _repository;

        public DeleteFreelancerCommandHandler(IFreelancerRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteFreelancerCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.Id);
        }
    }
}
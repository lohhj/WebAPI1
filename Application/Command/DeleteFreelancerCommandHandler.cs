using MediatR;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Application.Commands
{
    public class DeleteFreelancerCommandHandler(IFreelancerRepository repository) : IRequestHandler<DeleteFreelancerCommand, bool>
    {
        public async Task<bool> Handle(DeleteFreelancerCommand request, CancellationToken cancellationToken)
        {
            return await repository.DeleteAsync(request.Id);
        }
    }
}
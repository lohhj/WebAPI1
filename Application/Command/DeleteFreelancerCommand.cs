using MediatR;

namespace WebAPI1.Application.Commands
{
    public class DeleteFreelancerCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
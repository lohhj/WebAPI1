using FluentResults;
using MediatR;

namespace WebAPI1.Application.Commands
{
    public class DeleteFreelancerCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
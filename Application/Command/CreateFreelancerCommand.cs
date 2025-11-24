using MediatR;
using FluentResults;

namespace WebAPI1.Application.Commands;

public class CreateFreelancerCommand : FreelancerCommandBase, IRequest<Result<Guid>>
{
}
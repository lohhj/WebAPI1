using MediatR;
using WebAPI1.Application.DTOs;

namespace WebAPI1.Application.Queries
{
    public class GetAllFreelancersQuery : IRequest<IEnumerable<CreateFreelancerResponse>>
    {
    }
}
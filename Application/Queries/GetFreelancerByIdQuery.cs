using MediatR;
using WebAPI1.Application.DTOs;

namespace WebAPI1.Application.Queries
{
    public class GetFreelancerByIdQuery : IRequest<CreateFreelancerResponse?>
    {
        public int Id { get; set; }
    }
}
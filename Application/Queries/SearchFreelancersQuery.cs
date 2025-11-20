using MediatR;
using WebAPI1.Application.DTOs;

namespace WebAPI1.Application.Queries
{
    public class SearchFreelancersQuery : IRequest<IEnumerable<CreateFreelancerResponse>>
    {
        public string Keyword { get; set; } = string.Empty;
    }
}
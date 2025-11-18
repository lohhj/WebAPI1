using MediatR;
using System.Text.Json.Serialization;

namespace WebAPI1.Application.Commands
{
    public class ArchiveFreelancerCommand : IRequest<bool>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public bool Archived { get; set; }
    }
}
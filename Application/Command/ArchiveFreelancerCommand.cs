using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace WebAPI1.Application.Commands
{
    public class ArchiveFreelancerCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public bool Archived { get; set; }
    }
}
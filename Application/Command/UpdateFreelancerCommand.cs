using MediatR;
using FluentResults;
using System.Text.Json.Serialization;

namespace WebAPI1.Application.Commands;

public class UpdateFreelancerCommand : FreelancerCommandBase, IRequest<Result>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}
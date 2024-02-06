using Core.Application.Responses;

namespace Application.Features.Teams.Commands.Delete;

public class DeletedTeamResponse : IResponse
{
    public Guid Id { get; set; }
}
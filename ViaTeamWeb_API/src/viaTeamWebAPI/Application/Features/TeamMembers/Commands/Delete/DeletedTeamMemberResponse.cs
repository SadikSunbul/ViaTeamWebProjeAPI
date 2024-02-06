using Core.Application.Responses;

namespace Application.Features.TeamMembers.Commands.Delete;

public class DeletedTeamMemberResponse : IResponse
{
    public Guid Id { get; set; }
}
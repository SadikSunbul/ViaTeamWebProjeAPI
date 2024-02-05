using Core.Application.Responses;

namespace Application.Features.TeamMemberPresentations.Commands.Delete;

public class DeletedTeamMemberPresentationResponse : IResponse
{
    public Guid Id { get; set; }
}
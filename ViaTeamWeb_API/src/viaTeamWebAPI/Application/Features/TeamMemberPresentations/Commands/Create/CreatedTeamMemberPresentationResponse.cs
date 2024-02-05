using Core.Application.Responses;

namespace Application.Features.TeamMemberPresentations.Commands.Create;

public class CreatedTeamMemberPresentationResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Prg1 { get; set; }
    public string Prg2 { get; set; }
}
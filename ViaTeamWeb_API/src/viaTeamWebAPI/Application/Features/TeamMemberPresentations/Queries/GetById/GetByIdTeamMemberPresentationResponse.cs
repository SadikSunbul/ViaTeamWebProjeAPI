using Core.Application.Responses;

namespace Application.Features.TeamMemberPresentations.Queries.GetById;

public class GetByIdTeamMemberPresentationResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Prg1 { get; set; }
    public string Prg2 { get; set; }
}
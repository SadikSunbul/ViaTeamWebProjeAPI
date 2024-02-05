using Core.Application.Dtos;

namespace Application.Features.TeamMemberPresentations.Queries.GetList;

public class GetListTeamMemberPresentationListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Prg1 { get; set; }
    public string Prg2 { get; set; }
}
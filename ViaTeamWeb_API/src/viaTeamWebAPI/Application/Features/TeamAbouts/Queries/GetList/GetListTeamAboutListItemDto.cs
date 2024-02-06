using Core.Application.Dtos;

namespace Application.Features.TeamAbouts.Queries.GetList;

public class GetListTeamAboutListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Title1 { get; set; }
    public string Prg1 { get; set; }
    public string Title2 { get; set; }
    public string Prg2 { get; set; }
}
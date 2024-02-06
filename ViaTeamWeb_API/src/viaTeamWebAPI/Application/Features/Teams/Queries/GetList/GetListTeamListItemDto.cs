using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.Teams.Queries.GetList;

public class GetListTeamListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid TeamAboutId { get; set; }
    public TeamAbout? TeamAbout { get; set; }
}
using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.TeamMembers.Queries.GetList;

public class GetListTeamMemberListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public string PhoneNumber { get; set; }
    public string Presentation { get; set; }
    public Team? Team { get; set; }
}
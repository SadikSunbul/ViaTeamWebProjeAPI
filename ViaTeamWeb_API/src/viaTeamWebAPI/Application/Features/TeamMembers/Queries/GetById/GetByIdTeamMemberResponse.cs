using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.TeamMembers.Queries.GetById;

public class GetByIdTeamMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public string PhoneNumber { get; set; }
    public string Presentation { get; set; }
    public Team? Team { get; set; }
}
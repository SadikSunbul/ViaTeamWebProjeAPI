using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.TeamMembers.Commands.Update;

public class UpdatedTeamMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public string PhoneNumber { get; set; }
    public string Presentation { get; set; }
    public Team? Team { get; set; }
}
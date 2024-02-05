using Core.Application.Responses;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.Members.Commands.Create;

public class CreatedMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public string Job { get; set; }
    public string Country { get; set; }
    public string Authirize { get; set; }
    public TeamMember TeamMember { get; set; }
    public User User { get; set; }
}
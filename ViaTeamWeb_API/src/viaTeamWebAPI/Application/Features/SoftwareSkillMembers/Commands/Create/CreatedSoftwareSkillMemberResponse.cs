using Core.Application.Responses;

namespace Application.Features.SoftwareSkillMembers.Commands.Create;

public class CreatedSoftwareSkillMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SoftwareSkillId { get; set; }
    public Guid MemberId { get; set; }
    
}
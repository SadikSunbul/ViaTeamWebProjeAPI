using Core.Application.Responses;

namespace Application.Features.SoftwareSkillMembers.Commands.Update;

public class UpdatedSoftwareSkillMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SoftwareSkillId { get; set; }
    public Guid MemberId { get; set; }
   
}
using Core.Application.Responses;

namespace Application.Features.SoftwareSkillMembers.Queries.GetById;

public class GetByIdSoftwareSkillMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SoftwareSkillId { get; set; }
    public Guid MemberId { get; set; }
    
}
using Core.Application.Dtos;

namespace Application.Features.SoftwareSkillMembers.Queries.GetList;

public class GetListSoftwareSkillMemberListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid SoftwareSkillId { get; set; }
    public Guid MemberId { get; set; }
   
}
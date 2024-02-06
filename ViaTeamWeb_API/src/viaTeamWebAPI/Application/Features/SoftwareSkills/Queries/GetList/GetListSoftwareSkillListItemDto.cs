using Core.Application.Dtos;

namespace Application.Features.SoftwareSkills.Queries.GetList;

public class GetListSoftwareSkillListItemDto : IDto
{
    public Guid Id { get; set; }
    public string SkillName { get; set; }
    public string SkillPercent { get; set; }
    public Guid SoftwareSkillMemberId { get; set; }
}
using Core.Application.Responses;

namespace Application.Features.SoftwareSkills.Queries.GetById;

public class GetByIdSoftwareSkillResponse : IResponse
{
    public Guid Id { get; set; }
    public string SkillName { get; set; }
    public string SkillPercent { get; set; }
    public Guid SoftwareSkillMemberId { get; set; }
}
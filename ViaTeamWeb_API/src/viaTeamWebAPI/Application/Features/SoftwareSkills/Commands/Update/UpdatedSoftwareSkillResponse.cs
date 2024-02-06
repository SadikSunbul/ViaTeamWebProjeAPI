using Core.Application.Responses;

namespace Application.Features.SoftwareSkills.Commands.Update;

public class UpdatedSoftwareSkillResponse : IResponse
{
    public Guid Id { get; set; }
    public string SkillName { get; set; }
    public string SkillPercent { get; set; }
    public Guid MemberId { get; set; }
}
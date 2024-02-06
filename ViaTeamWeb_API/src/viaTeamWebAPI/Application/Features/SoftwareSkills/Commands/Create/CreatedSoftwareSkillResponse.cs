using Core.Application.Responses;

namespace Application.Features.SoftwareSkills.Commands.Create;

public class CreatedSoftwareSkillResponse : IResponse
{
    public Guid Id { get; set; }
    public string SkillName { get; set; }
    public string SkillPercent { get; set; }
    public Guid MemberId { get; set; }
}
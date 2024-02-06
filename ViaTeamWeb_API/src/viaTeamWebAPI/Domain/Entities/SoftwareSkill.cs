using Core.Persistence.Repositories;

namespace Domain.Entities;

public class SoftwareSkill:Entity<Guid>
{
    public string SkillName { get; set; }
    public string SkillPercent { get; set; }
    public Guid? SoftwareSkillMemberId { get; set; }
    public ICollection<SoftwareSkillMember>? Members { get; set; }
}
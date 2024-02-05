using Core.Persistence.Repositories;

namespace Domain.Entities;

public class SoftwareSkill:Entity<Guid>
{
    public string SkillName { get; set; }
    public string SkillPercent { get; set; }
    public Guid MemberId { get; set; }
    public Member Member { get; set; }
}
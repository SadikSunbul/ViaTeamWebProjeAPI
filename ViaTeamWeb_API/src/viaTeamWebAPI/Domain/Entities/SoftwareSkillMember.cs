using Core.Persistence.Repositories;

namespace Domain.Entities;

public class SoftwareSkillMember:Entity<Guid>
{
    public Guid SoftwareSkillId { get; set; }
    public Guid MemberId { get; set; }
    public SoftwareSkill SoftwareSkill { get; set; }
    public Member Member { get; set; }
}
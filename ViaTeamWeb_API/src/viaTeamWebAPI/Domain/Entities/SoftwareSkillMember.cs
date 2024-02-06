using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class SoftwareSkillMember : Entity<Guid>
{
    public Guid SoftwareSkillId { get; set; }
    public Guid MemberId { get; set; }

    [NotMapped] public SoftwareSkill? SoftwareSkill { get; set; }
    [NotMapped] public Member? Member { get; set; }
}
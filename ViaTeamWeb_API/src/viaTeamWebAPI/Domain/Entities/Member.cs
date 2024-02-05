using Core.Persistence.Repositories;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Entities;

public class Member:Entity<Guid>
{
    public int UserId { get; set; }
    public string Job { get; set; }
    public string Country { get; set; }
    public ICollection<BusinessArea> BusinessAreas { get; set; }
    public ICollection<SoftwareSkill> SoftwareSkills { get; set; }
    public ICollection<ExternalLink> ExternalLinks { get; set; }
    public ICollection<Member> Flovers { get; set; }

    public Member()
    {
        BusinessAreas = new List<BusinessArea>();
        SoftwareSkills = new List<SoftwareSkill>();
        ExternalLinks = new List<ExternalLink>();
        Flovers = new List<Member>();
    }
}
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Entities;

public class Member:Entity<Guid>
{
    public int UserId { get; set; }
    public string? Job { get; set; }
    public string? Country { get; set; }
    public string? Authirize { get; set; }
    public ICollection<BusinessAreaMember>? BusinessAreas { get; set; }
    public ICollection<SoftwareSkillMember>? SoftwareSkills { get; set; }
    public ICollection<ExternalLink>? ExternalLinks { get; set; }
    public ICollection<Member>? Flovers { get; set; }
    public TeamMember? TeamMember { get; set; }
    public User? User { get; set; }

    public Member()
    {
        BusinessAreas = new List<BusinessAreaMember>();
        SoftwareSkills = new List<SoftwareSkillMember>();
        ExternalLinks = new List<ExternalLink>();
        Flovers = new List<Member>();
    }
}
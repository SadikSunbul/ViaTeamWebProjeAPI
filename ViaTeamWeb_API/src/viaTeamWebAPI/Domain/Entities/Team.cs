using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;

public class Team:Entity<Guid>
{
    public string Name { get; set; }
    public Guid TeamAboutId { get; set; }
    public ICollection<TeamMember>? TeamMembers { get; set; }
    public ICollection<ExternalLink>? TeamSocialsMedias { get; set; }
    public TeamAbout? TeamAbout { get; set; }

    public Team()
    {
        TeamMembers = new List<TeamMember>();
        TeamSocialsMedias = new List<ExternalLink>();
    }
}
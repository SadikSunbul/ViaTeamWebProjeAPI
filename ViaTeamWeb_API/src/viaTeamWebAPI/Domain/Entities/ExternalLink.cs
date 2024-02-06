using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ExternalLink:Entity<Guid>
{
    public string Name { get; set; }
    public string Url { get; set; }
    public Guid MemberId { get; set; }
    public Guid TeamId { get; set; }
    public Member Member { get; set; }
    [NotMapped] public Team Team { get; set; }

    
}
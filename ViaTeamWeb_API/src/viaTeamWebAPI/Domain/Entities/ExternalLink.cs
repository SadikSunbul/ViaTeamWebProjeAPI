using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ExternalLink:Entity<Guid>
{
    public string Name { get; set; }
    public string Url { get; set; }
    public Guid MemberId { get; set; }
    public Member Member { get; set; }
    
}
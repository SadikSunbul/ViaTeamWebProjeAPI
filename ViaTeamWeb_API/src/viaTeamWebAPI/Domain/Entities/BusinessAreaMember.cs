using Core.Persistence.Repositories;

namespace Domain.Entities;

public class BusinessAreaMember:Entity<Guid>
{
    public Guid BusinessAreaId { get; set; }
    public Guid MemberId { get; set; }
    public BusinessArea BusinessArea { get; set; }
    public Member Member { get; set; }
}
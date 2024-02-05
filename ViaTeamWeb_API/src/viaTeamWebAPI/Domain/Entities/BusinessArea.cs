using Core.Persistence.Repositories;

namespace Domain.Entities;

public class BusinessArea:Entity<Guid>
{
    public string Name { get; set; }
    public Guid MemberId { get; set; }
    public ICollection<BusinessAreaMember> Members { get; set; }

    public BusinessArea()
    {
        Members = new List<BusinessAreaMember>();
    }
}
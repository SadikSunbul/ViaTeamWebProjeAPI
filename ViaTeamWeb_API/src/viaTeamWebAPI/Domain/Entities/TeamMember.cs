using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;

public class TeamMember:Entity<Guid>
{
    public string PhoneNumber { get; set; }
    public string Presentation { get; set; }
    public User User { get; set; }
}
using Core.Persistence.Repositories;
using Core.Security.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class TeamMember : Entity<Guid>
{
    public Guid TeamId { get; set; }
    public string PhoneNumber { get; set; }
    public string Presentation { get; set; }
    [NotMapped] public Team? Team { get; set; }
}
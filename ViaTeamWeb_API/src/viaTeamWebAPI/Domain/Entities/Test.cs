using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Test : Entity<Guid>
{
    public string NAME { get; set; }
}
using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Deneme : Entity<Guid>
{
    public string Name { get; set; }
}
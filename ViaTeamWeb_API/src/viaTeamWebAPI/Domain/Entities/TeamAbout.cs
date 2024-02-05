using Core.Persistence.Repositories;

namespace Domain.Entities;

public class TeamAbout:Entity<Guid>
{
    public string title1 { get; set; }
    public string prg1 { get; set; }
    public string title2 { get; set; }
    public string prg2 { get; set; }
}
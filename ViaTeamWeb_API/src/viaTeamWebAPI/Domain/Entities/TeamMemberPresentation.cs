using Core.Persistence.Repositories;

namespace Domain.Entities;

public class TeamMemberPresentation:Entity<Guid>
{
    public string Title { get; set; }
    public string prg1 { get; set; }
    public string prg2 { get; set; }
}
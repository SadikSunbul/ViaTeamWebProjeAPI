using Core.Persistence.Repositories;

namespace Domain.Entities;

public class HeroSectionWrite:Entity<Guid>
{
    public string Title { get; set; }
    public string Paragraph { get; set; }
    public string ButtonText { get; set; }
}
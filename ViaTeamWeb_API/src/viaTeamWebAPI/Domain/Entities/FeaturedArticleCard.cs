using Core.Persistence.Repositories;

namespace Domain.Entities;

public class FeaturedArticleCard:Entity<Guid>
{
    public string Title { get; set; }
    public string Explanation { get; set; }
    public string Writer { get; set; }
    public Guid FeaturedSectionEntitiesId { get; set; }
    public FeaturedSectionEntities FeaturedSectionEntities { get; set; }
}
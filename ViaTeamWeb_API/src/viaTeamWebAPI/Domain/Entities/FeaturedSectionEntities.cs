using Core.Persistence.Repositories;

namespace Domain.Entities;

public class FeaturedSectionEntities:Entity<Guid>
{
    public string SmallTitle { get; set; }
    public string Title { get; set; }
    public ICollection<FeaturedArticleCard> FeturedArticleCards { get; set; }

    public FeaturedSectionEntities()
    {
        FeturedArticleCards = new List<FeaturedArticleCard>();
    }
}
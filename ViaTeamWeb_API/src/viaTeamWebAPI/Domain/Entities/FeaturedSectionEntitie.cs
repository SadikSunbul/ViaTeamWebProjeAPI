using Core.Persistence.Repositories;

namespace Domain.Entities;

public class FeaturedSectionEntitie:Entity<Guid>
{
    public string SmallTitle { get; set; }
    public string Title { get; set; }
    public ICollection<FeaturedArticleCard> FeturedArticleCards { get; set; }

    public FeaturedSectionEntitie()
    {
        FeturedArticleCards = new List<FeaturedArticleCard>();
    }
}
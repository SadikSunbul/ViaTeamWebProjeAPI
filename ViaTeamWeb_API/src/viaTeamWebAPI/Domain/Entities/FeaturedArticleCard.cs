using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class FeaturedArticleCard:Entity<Guid>
{
    public string Title { get; set; }
    public string Explanation { get; set; }
    public string Writer { get; set; }
    public Guid FeaturedSectionEntitiesId { get; set; }
    [NotMapped]
    public FeaturedSectionEntitie FeaturedSectionEntitie { get; set; }
}
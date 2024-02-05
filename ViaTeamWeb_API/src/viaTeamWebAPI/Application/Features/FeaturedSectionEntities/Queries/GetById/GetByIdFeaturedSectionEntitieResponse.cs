using Core.Application.Responses;
using Domain.Entities;
using System.Collections;

namespace Application.Features.FeaturedSectionEntities.Queries.GetById;

public class GetByIdFeaturedSectionEntitieResponse : IResponse
{
    public Guid Id { get; set; }
    public string SmallTitle { get; set; }
    public string Title { get; set; }
    public ICollection<FeaturedArticleCard> EFeaturedArticleCards { get; set; }
}
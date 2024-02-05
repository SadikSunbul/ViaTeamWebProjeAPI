using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.FeaturedArticleCards.Commands.Update;

public class UpdatedFeaturedArticleCardResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Explanation { get; set; }
    public string Writer { get; set; }
    public Guid FeaturedSectionEntitiesId { get; set; }
    public FeaturedSectionEntitie FeaturedSectionEntitie { get; set; }
}
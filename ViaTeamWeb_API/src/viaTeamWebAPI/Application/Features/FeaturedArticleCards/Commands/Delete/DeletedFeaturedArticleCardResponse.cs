using Core.Application.Responses;

namespace Application.Features.FeaturedArticleCards.Commands.Delete;

public class DeletedFeaturedArticleCardResponse : IResponse
{
    public Guid Id { get; set; }
}
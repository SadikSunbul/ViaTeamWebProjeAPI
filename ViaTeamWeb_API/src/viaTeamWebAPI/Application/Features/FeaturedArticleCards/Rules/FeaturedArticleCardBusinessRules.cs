using Application.Features.FeaturedArticleCards.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.FeaturedArticleCards.Rules;

public class FeaturedArticleCardBusinessRules : BaseBusinessRules
{
    private readonly IFeaturedArticleCardRepository _featuredArticleCardRepository;

    public FeaturedArticleCardBusinessRules(IFeaturedArticleCardRepository featuredArticleCardRepository)
    {
        _featuredArticleCardRepository = featuredArticleCardRepository;
    }

    public Task FeaturedArticleCardShouldExistWhenSelected(FeaturedArticleCard? featuredArticleCard)
    {
        if (featuredArticleCard == null)
            throw new BusinessException(FeaturedArticleCardsBusinessMessages.FeaturedArticleCardNotExists);
        return Task.CompletedTask;
    }

    public async Task FeaturedArticleCardIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        FeaturedArticleCard? featuredArticleCard = await _featuredArticleCardRepository.GetAsync(
            predicate: fac => fac.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await FeaturedArticleCardShouldExistWhenSelected(featuredArticleCard);
    }
}
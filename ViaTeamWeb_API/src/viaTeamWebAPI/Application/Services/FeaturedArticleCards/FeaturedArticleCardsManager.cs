using Application.Features.FeaturedArticleCards.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.FeaturedArticleCards;

public class FeaturedArticleCardsManager : IFeaturedArticleCardsService
{
    private readonly IFeaturedArticleCardRepository _featuredArticleCardRepository;
    private readonly FeaturedArticleCardBusinessRules _featuredArticleCardBusinessRules;

    public FeaturedArticleCardsManager(IFeaturedArticleCardRepository featuredArticleCardRepository, FeaturedArticleCardBusinessRules featuredArticleCardBusinessRules)
    {
        _featuredArticleCardRepository = featuredArticleCardRepository;
        _featuredArticleCardBusinessRules = featuredArticleCardBusinessRules;
    }

    public async Task<FeaturedArticleCard?> GetAsync(
        Expression<Func<FeaturedArticleCard, bool>> predicate,
        Func<IQueryable<FeaturedArticleCard>, IIncludableQueryable<FeaturedArticleCard, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        FeaturedArticleCard? featuredArticleCard = await _featuredArticleCardRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return featuredArticleCard;
    }

    public async Task<IPaginate<FeaturedArticleCard>?> GetListAsync(
        Expression<Func<FeaturedArticleCard, bool>>? predicate = null,
        Func<IQueryable<FeaturedArticleCard>, IOrderedQueryable<FeaturedArticleCard>>? orderBy = null,
        Func<IQueryable<FeaturedArticleCard>, IIncludableQueryable<FeaturedArticleCard, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<FeaturedArticleCard> featuredArticleCardList = await _featuredArticleCardRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return featuredArticleCardList;
    }

    public async Task<FeaturedArticleCard> AddAsync(FeaturedArticleCard featuredArticleCard)
    {
        FeaturedArticleCard addedFeaturedArticleCard = await _featuredArticleCardRepository.AddAsync(featuredArticleCard);

        return addedFeaturedArticleCard;
    }

    public async Task<FeaturedArticleCard> UpdateAsync(FeaturedArticleCard featuredArticleCard)
    {
        FeaturedArticleCard updatedFeaturedArticleCard = await _featuredArticleCardRepository.UpdateAsync(featuredArticleCard);

        return updatedFeaturedArticleCard;
    }

    public async Task<FeaturedArticleCard> DeleteAsync(FeaturedArticleCard featuredArticleCard, bool permanent = false)
    {
        FeaturedArticleCard deletedFeaturedArticleCard = await _featuredArticleCardRepository.DeleteAsync(featuredArticleCard);

        return deletedFeaturedArticleCard;
    }
}

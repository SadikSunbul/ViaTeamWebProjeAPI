using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.FeaturedArticleCards;

public interface IFeaturedArticleCardsService
{
    Task<FeaturedArticleCard?> GetAsync(
        Expression<Func<FeaturedArticleCard, bool>> predicate,
        Func<IQueryable<FeaturedArticleCard>, IIncludableQueryable<FeaturedArticleCard, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<FeaturedArticleCard>?> GetListAsync(
        Expression<Func<FeaturedArticleCard, bool>>? predicate = null,
        Func<IQueryable<FeaturedArticleCard>, IOrderedQueryable<FeaturedArticleCard>>? orderBy = null,
        Func<IQueryable<FeaturedArticleCard>, IIncludableQueryable<FeaturedArticleCard, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<FeaturedArticleCard> AddAsync(FeaturedArticleCard featuredArticleCard);
    Task<FeaturedArticleCard> UpdateAsync(FeaturedArticleCard featuredArticleCard);
    Task<FeaturedArticleCard> DeleteAsync(FeaturedArticleCard featuredArticleCard, bool permanent = false);
}

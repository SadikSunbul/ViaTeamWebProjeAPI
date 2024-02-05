using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.FeaturedSectionEntities;

public interface IFeaturedSectionEntitiesService
{
    Task<FeaturedSectionEntitie?> GetAsync(
        Expression<Func<FeaturedSectionEntitie, bool>> predicate,
        Func<IQueryable<FeaturedSectionEntitie>, IIncludableQueryable<FeaturedSectionEntitie, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<FeaturedSectionEntitie>?> GetListAsync(
        Expression<Func<FeaturedSectionEntitie, bool>>? predicate = null,
        Func<IQueryable<FeaturedSectionEntitie>, IOrderedQueryable<FeaturedSectionEntitie>>? orderBy = null,
        Func<IQueryable<FeaturedSectionEntitie>, IIncludableQueryable<FeaturedSectionEntitie, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<FeaturedSectionEntitie> AddAsync(FeaturedSectionEntitie featuredSectionEntitie);
    Task<FeaturedSectionEntitie> UpdateAsync(FeaturedSectionEntitie featuredSectionEntitie);
    Task<FeaturedSectionEntitie> DeleteAsync(FeaturedSectionEntitie featuredSectionEntitie, bool permanent = false);
}

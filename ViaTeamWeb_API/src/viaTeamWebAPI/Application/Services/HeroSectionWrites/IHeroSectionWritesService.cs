using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.HeroSectionWrites;

public interface IHeroSectionWritesService
{
    Task<HeroSectionWrite?> GetAsync(
        Expression<Func<HeroSectionWrite, bool>> predicate,
        Func<IQueryable<HeroSectionWrite>, IIncludableQueryable<HeroSectionWrite, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<HeroSectionWrite>?> GetListAsync(
        Expression<Func<HeroSectionWrite, bool>>? predicate = null,
        Func<IQueryable<HeroSectionWrite>, IOrderedQueryable<HeroSectionWrite>>? orderBy = null,
        Func<IQueryable<HeroSectionWrite>, IIncludableQueryable<HeroSectionWrite, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<HeroSectionWrite> AddAsync(HeroSectionWrite heroSectionWrite);
    Task<HeroSectionWrite> UpdateAsync(HeroSectionWrite heroSectionWrite);
    Task<HeroSectionWrite> DeleteAsync(HeroSectionWrite heroSectionWrite, bool permanent = false);
}

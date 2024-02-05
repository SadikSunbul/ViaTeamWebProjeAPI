using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.BusinessAreas;

public interface IBusinessAreasService
{
    Task<BusinessArea?> GetAsync(
        Expression<Func<BusinessArea, bool>> predicate,
        Func<IQueryable<BusinessArea>, IIncludableQueryable<BusinessArea, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<BusinessArea>?> GetListAsync(
        Expression<Func<BusinessArea, bool>>? predicate = null,
        Func<IQueryable<BusinessArea>, IOrderedQueryable<BusinessArea>>? orderBy = null,
        Func<IQueryable<BusinessArea>, IIncludableQueryable<BusinessArea, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<BusinessArea> AddAsync(BusinessArea businessArea);
    Task<BusinessArea> UpdateAsync(BusinessArea businessArea);
    Task<BusinessArea> DeleteAsync(BusinessArea businessArea, bool permanent = false);
}

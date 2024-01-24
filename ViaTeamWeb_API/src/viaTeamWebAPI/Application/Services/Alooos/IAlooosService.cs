using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Alooos;

public interface IAlooosService
{
    Task<Alooo?> GetAsync(
        Expression<Func<Alooo, bool>> predicate,
        Func<IQueryable<Alooo>, IIncludableQueryable<Alooo, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Alooo>?> GetListAsync(
        Expression<Func<Alooo, bool>>? predicate = null,
        Func<IQueryable<Alooo>, IOrderedQueryable<Alooo>>? orderBy = null,
        Func<IQueryable<Alooo>, IIncludableQueryable<Alooo, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Alooo> AddAsync(Alooo alooo);
    Task<Alooo> UpdateAsync(Alooo alooo);
    Task<Alooo> DeleteAsync(Alooo alooo, bool permanent = false);
}

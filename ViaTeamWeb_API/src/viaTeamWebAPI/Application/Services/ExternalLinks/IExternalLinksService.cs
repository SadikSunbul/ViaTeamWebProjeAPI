using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ExternalLinks;

public interface IExternalLinksService
{
    Task<ExternalLink?> GetAsync(
        Expression<Func<ExternalLink, bool>> predicate,
        Func<IQueryable<ExternalLink>, IIncludableQueryable<ExternalLink, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ExternalLink>?> GetListAsync(
        Expression<Func<ExternalLink, bool>>? predicate = null,
        Func<IQueryable<ExternalLink>, IOrderedQueryable<ExternalLink>>? orderBy = null,
        Func<IQueryable<ExternalLink>, IIncludableQueryable<ExternalLink, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ExternalLink> AddAsync(ExternalLink externalLink);
    Task<ExternalLink> UpdateAsync(ExternalLink externalLink);
    Task<ExternalLink> DeleteAsync(ExternalLink externalLink, bool permanent = false);
}

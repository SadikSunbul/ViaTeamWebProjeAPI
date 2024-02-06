using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TeamAbouts;

public interface ITeamAboutsService
{
    Task<TeamAbout?> GetAsync(
        Expression<Func<TeamAbout, bool>> predicate,
        Func<IQueryable<TeamAbout>, IIncludableQueryable<TeamAbout, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<TeamAbout>?> GetListAsync(
        Expression<Func<TeamAbout, bool>>? predicate = null,
        Func<IQueryable<TeamAbout>, IOrderedQueryable<TeamAbout>>? orderBy = null,
        Func<IQueryable<TeamAbout>, IIncludableQueryable<TeamAbout, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<TeamAbout> AddAsync(TeamAbout teamAbout);
    Task<TeamAbout> UpdateAsync(TeamAbout teamAbout);
    Task<TeamAbout> DeleteAsync(TeamAbout teamAbout, bool permanent = false);
}

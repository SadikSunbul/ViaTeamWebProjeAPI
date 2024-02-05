using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TeamMemberPresentations;

public interface ITeamMemberPresentationsService
{
    Task<TeamMemberPresentation?> GetAsync(
        Expression<Func<TeamMemberPresentation, bool>> predicate,
        Func<IQueryable<TeamMemberPresentation>, IIncludableQueryable<TeamMemberPresentation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<TeamMemberPresentation>?> GetListAsync(
        Expression<Func<TeamMemberPresentation, bool>>? predicate = null,
        Func<IQueryable<TeamMemberPresentation>, IOrderedQueryable<TeamMemberPresentation>>? orderBy = null,
        Func<IQueryable<TeamMemberPresentation>, IIncludableQueryable<TeamMemberPresentation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<TeamMemberPresentation> AddAsync(TeamMemberPresentation teamMemberPresentation);
    Task<TeamMemberPresentation> UpdateAsync(TeamMemberPresentation teamMemberPresentation);
    Task<TeamMemberPresentation> DeleteAsync(TeamMemberPresentation teamMemberPresentation, bool permanent = false);
}

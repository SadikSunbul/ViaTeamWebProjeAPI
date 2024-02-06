using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TeamMembers;

public interface ITeamMembersService
{
    Task<TeamMember?> GetAsync(
        Expression<Func<TeamMember, bool>> predicate,
        Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<TeamMember>?> GetListAsync(
        Expression<Func<TeamMember, bool>>? predicate = null,
        Func<IQueryable<TeamMember>, IOrderedQueryable<TeamMember>>? orderBy = null,
        Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<TeamMember> AddAsync(TeamMember teamMember);
    Task<TeamMember> UpdateAsync(TeamMember teamMember);
    Task<TeamMember> DeleteAsync(TeamMember teamMember, bool permanent = false);
}

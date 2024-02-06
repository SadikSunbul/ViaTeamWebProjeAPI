using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SoftwareSkillMembers;

public interface ISoftwareSkillMembersService
{
    Task<SoftwareSkillMember?> GetAsync(
        Expression<Func<SoftwareSkillMember, bool>> predicate,
        Func<IQueryable<SoftwareSkillMember>, IIncludableQueryable<SoftwareSkillMember, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<SoftwareSkillMember>?> GetListAsync(
        Expression<Func<SoftwareSkillMember, bool>>? predicate = null,
        Func<IQueryable<SoftwareSkillMember>, IOrderedQueryable<SoftwareSkillMember>>? orderBy = null,
        Func<IQueryable<SoftwareSkillMember>, IIncludableQueryable<SoftwareSkillMember, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<SoftwareSkillMember> AddAsync(SoftwareSkillMember softwareSkillMember);
    Task<SoftwareSkillMember> UpdateAsync(SoftwareSkillMember softwareSkillMember);
    Task<SoftwareSkillMember> DeleteAsync(SoftwareSkillMember softwareSkillMember, bool permanent = false);
}

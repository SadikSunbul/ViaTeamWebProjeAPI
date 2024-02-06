using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SoftwareSkills;

public interface ISoftwareSkillsService
{
    Task<SoftwareSkill?> GetAsync(
        Expression<Func<SoftwareSkill, bool>> predicate,
        Func<IQueryable<SoftwareSkill>, IIncludableQueryable<SoftwareSkill, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<SoftwareSkill>?> GetListAsync(
        Expression<Func<SoftwareSkill, bool>>? predicate = null,
        Func<IQueryable<SoftwareSkill>, IOrderedQueryable<SoftwareSkill>>? orderBy = null,
        Func<IQueryable<SoftwareSkill>, IIncludableQueryable<SoftwareSkill, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<SoftwareSkill> AddAsync(SoftwareSkill softwareSkill);
    Task<SoftwareSkill> UpdateAsync(SoftwareSkill softwareSkill);
    Task<SoftwareSkill> DeleteAsync(SoftwareSkill softwareSkill, bool permanent = false);
}

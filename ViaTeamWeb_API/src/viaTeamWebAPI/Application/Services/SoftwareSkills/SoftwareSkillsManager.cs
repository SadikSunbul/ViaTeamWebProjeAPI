using Application.Features.SoftwareSkills.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SoftwareSkills;

public class SoftwareSkillsManager : ISoftwareSkillsService
{
    private readonly ISoftwareSkillRepository _softwareSkillRepository;
    private readonly SoftwareSkillBusinessRules _softwareSkillBusinessRules;

    public SoftwareSkillsManager(ISoftwareSkillRepository softwareSkillRepository, SoftwareSkillBusinessRules softwareSkillBusinessRules)
    {
        _softwareSkillRepository = softwareSkillRepository;
        _softwareSkillBusinessRules = softwareSkillBusinessRules;
    }

    public async Task<SoftwareSkill?> GetAsync(
        Expression<Func<SoftwareSkill, bool>> predicate,
        Func<IQueryable<SoftwareSkill>, IIncludableQueryable<SoftwareSkill, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        SoftwareSkill? softwareSkill = await _softwareSkillRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return softwareSkill;
    }

    public async Task<IPaginate<SoftwareSkill>?> GetListAsync(
        Expression<Func<SoftwareSkill, bool>>? predicate = null,
        Func<IQueryable<SoftwareSkill>, IOrderedQueryable<SoftwareSkill>>? orderBy = null,
        Func<IQueryable<SoftwareSkill>, IIncludableQueryable<SoftwareSkill, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<SoftwareSkill> softwareSkillList = await _softwareSkillRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return softwareSkillList;
    }

    public async Task<SoftwareSkill> AddAsync(SoftwareSkill softwareSkill)
    {
        SoftwareSkill addedSoftwareSkill = await _softwareSkillRepository.AddAsync(softwareSkill);

        return addedSoftwareSkill;
    }

    public async Task<SoftwareSkill> UpdateAsync(SoftwareSkill softwareSkill)
    {
        SoftwareSkill updatedSoftwareSkill = await _softwareSkillRepository.UpdateAsync(softwareSkill);

        return updatedSoftwareSkill;
    }

    public async Task<SoftwareSkill> DeleteAsync(SoftwareSkill softwareSkill, bool permanent = false)
    {
        SoftwareSkill deletedSoftwareSkill = await _softwareSkillRepository.DeleteAsync(softwareSkill);

        return deletedSoftwareSkill;
    }
}

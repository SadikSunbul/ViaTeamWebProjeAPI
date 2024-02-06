using Application.Features.SoftwareSkillMembers.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SoftwareSkillMembers;

public class SoftwareSkillMembersManager : ISoftwareSkillMembersService
{
    private readonly ISoftwareSkillMemberRepository _softwareSkillMemberRepository;
    private readonly SoftwareSkillMemberBusinessRules _softwareSkillMemberBusinessRules;

    public SoftwareSkillMembersManager(ISoftwareSkillMemberRepository softwareSkillMemberRepository, SoftwareSkillMemberBusinessRules softwareSkillMemberBusinessRules)
    {
        _softwareSkillMemberRepository = softwareSkillMemberRepository;
        _softwareSkillMemberBusinessRules = softwareSkillMemberBusinessRules;
    }

    public async Task<SoftwareSkillMember?> GetAsync(
        Expression<Func<SoftwareSkillMember, bool>> predicate,
        Func<IQueryable<SoftwareSkillMember>, IIncludableQueryable<SoftwareSkillMember, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        SoftwareSkillMember? softwareSkillMember = await _softwareSkillMemberRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return softwareSkillMember;
    }

    public async Task<IPaginate<SoftwareSkillMember>?> GetListAsync(
        Expression<Func<SoftwareSkillMember, bool>>? predicate = null,
        Func<IQueryable<SoftwareSkillMember>, IOrderedQueryable<SoftwareSkillMember>>? orderBy = null,
        Func<IQueryable<SoftwareSkillMember>, IIncludableQueryable<SoftwareSkillMember, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<SoftwareSkillMember> softwareSkillMemberList = await _softwareSkillMemberRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return softwareSkillMemberList;
    }

    public async Task<SoftwareSkillMember> AddAsync(SoftwareSkillMember softwareSkillMember)
    {
        SoftwareSkillMember addedSoftwareSkillMember = await _softwareSkillMemberRepository.AddAsync(softwareSkillMember);

        return addedSoftwareSkillMember;
    }

    public async Task<SoftwareSkillMember> UpdateAsync(SoftwareSkillMember softwareSkillMember)
    {
        SoftwareSkillMember updatedSoftwareSkillMember = await _softwareSkillMemberRepository.UpdateAsync(softwareSkillMember);

        return updatedSoftwareSkillMember;
    }

    public async Task<SoftwareSkillMember> DeleteAsync(SoftwareSkillMember softwareSkillMember, bool permanent = false)
    {
        SoftwareSkillMember deletedSoftwareSkillMember = await _softwareSkillMemberRepository.DeleteAsync(softwareSkillMember);

        return deletedSoftwareSkillMember;
    }
}

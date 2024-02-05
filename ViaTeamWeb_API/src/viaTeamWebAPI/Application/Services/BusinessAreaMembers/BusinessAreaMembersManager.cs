using Application.Features.BusinessAreaMembers.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.BusinessAreaMembers;

public class BusinessAreaMembersManager : IBusinessAreaMembersService
{
    private readonly IBusinessAreaMemberRepository _businessAreaMemberRepository;
    private readonly BusinessAreaMemberBusinessRules _businessAreaMemberBusinessRules;

    public BusinessAreaMembersManager(IBusinessAreaMemberRepository businessAreaMemberRepository, BusinessAreaMemberBusinessRules businessAreaMemberBusinessRules)
    {
        _businessAreaMemberRepository = businessAreaMemberRepository;
        _businessAreaMemberBusinessRules = businessAreaMemberBusinessRules;
    }

    public async Task<BusinessAreaMember?> GetAsync(
        Expression<Func<BusinessAreaMember, bool>> predicate,
        Func<IQueryable<BusinessAreaMember>, IIncludableQueryable<BusinessAreaMember, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        BusinessAreaMember? businessAreaMember = await _businessAreaMemberRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return businessAreaMember;
    }

    public async Task<IPaginate<BusinessAreaMember>?> GetListAsync(
        Expression<Func<BusinessAreaMember, bool>>? predicate = null,
        Func<IQueryable<BusinessAreaMember>, IOrderedQueryable<BusinessAreaMember>>? orderBy = null,
        Func<IQueryable<BusinessAreaMember>, IIncludableQueryable<BusinessAreaMember, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<BusinessAreaMember> businessAreaMemberList = await _businessAreaMemberRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return businessAreaMemberList;
    }

    public async Task<BusinessAreaMember> AddAsync(BusinessAreaMember businessAreaMember)
    {
        BusinessAreaMember addedBusinessAreaMember = await _businessAreaMemberRepository.AddAsync(businessAreaMember);

        return addedBusinessAreaMember;
    }

    public async Task<BusinessAreaMember> UpdateAsync(BusinessAreaMember businessAreaMember)
    {
        BusinessAreaMember updatedBusinessAreaMember = await _businessAreaMemberRepository.UpdateAsync(businessAreaMember);

        return updatedBusinessAreaMember;
    }

    public async Task<BusinessAreaMember> DeleteAsync(BusinessAreaMember businessAreaMember, bool permanent = false)
    {
        BusinessAreaMember deletedBusinessAreaMember = await _businessAreaMemberRepository.DeleteAsync(businessAreaMember);

        return deletedBusinessAreaMember;
    }
}

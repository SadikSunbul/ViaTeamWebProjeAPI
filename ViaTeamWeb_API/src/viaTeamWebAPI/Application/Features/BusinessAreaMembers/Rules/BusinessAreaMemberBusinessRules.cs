using Application.Features.BusinessAreaMembers.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.BusinessAreaMembers.Rules;

public class BusinessAreaMemberBusinessRules : BaseBusinessRules
{
    private readonly IBusinessAreaMemberRepository _businessAreaMemberRepository;

    public BusinessAreaMemberBusinessRules(IBusinessAreaMemberRepository businessAreaMemberRepository)
    {
        _businessAreaMemberRepository = businessAreaMemberRepository;
    }

    public Task BusinessAreaMemberShouldExistWhenSelected(BusinessAreaMember? businessAreaMember)
    {
        if (businessAreaMember == null)
            throw new BusinessException(BusinessAreaMembersBusinessMessages.BusinessAreaMemberNotExists);
        return Task.CompletedTask;
    }

    public async Task BusinessAreaMemberIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        BusinessAreaMember? businessAreaMember = await _businessAreaMemberRepository.GetAsync(
            predicate: bam => bam.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BusinessAreaMemberShouldExistWhenSelected(businessAreaMember);
    }
}
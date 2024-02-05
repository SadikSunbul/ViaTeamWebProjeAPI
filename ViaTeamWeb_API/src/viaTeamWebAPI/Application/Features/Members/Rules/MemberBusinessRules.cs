using Application.Features.Members.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Members.Rules;

public class MemberBusinessRules : BaseBusinessRules
{
    private readonly IMemberRepository _memberRepository;

    public MemberBusinessRules(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public Task MemberShouldExistWhenSelected(Member? member)
    {
        if (member == null)
            throw new BusinessException(MembersBusinessMessages.MemberNotExists);
        return Task.CompletedTask;
    }

    public async Task MemberIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Member? member = await _memberRepository.GetAsync(
            predicate: m => m.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await MemberShouldExistWhenSelected(member);
    }
}
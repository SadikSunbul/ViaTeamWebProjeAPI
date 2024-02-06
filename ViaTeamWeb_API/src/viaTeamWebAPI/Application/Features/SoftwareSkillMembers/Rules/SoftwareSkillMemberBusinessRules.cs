using Application.Features.SoftwareSkillMembers.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.SoftwareSkillMembers.Rules;

public class SoftwareSkillMemberBusinessRules : BaseBusinessRules
{
    private readonly ISoftwareSkillMemberRepository _softwareSkillMemberRepository;

    public SoftwareSkillMemberBusinessRules(ISoftwareSkillMemberRepository softwareSkillMemberRepository)
    {
        _softwareSkillMemberRepository = softwareSkillMemberRepository;
    }

    public Task SoftwareSkillMemberShouldExistWhenSelected(SoftwareSkillMember? softwareSkillMember)
    {
        if (softwareSkillMember == null)
            throw new BusinessException(SoftwareSkillMembersBusinessMessages.SoftwareSkillMemberNotExists);
        return Task.CompletedTask;
    }

    public async Task SoftwareSkillMemberIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        SoftwareSkillMember? softwareSkillMember = await _softwareSkillMemberRepository.GetAsync(
            predicate: ssm => ssm.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SoftwareSkillMemberShouldExistWhenSelected(softwareSkillMember);
    }
}
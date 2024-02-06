using Application.Features.SoftwareSkills.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.SoftwareSkills.Rules;

public class SoftwareSkillBusinessRules : BaseBusinessRules
{
    private readonly ISoftwareSkillRepository _softwareSkillRepository;

    public SoftwareSkillBusinessRules(ISoftwareSkillRepository softwareSkillRepository)
    {
        _softwareSkillRepository = softwareSkillRepository;
    }

    public Task SoftwareSkillShouldExistWhenSelected(SoftwareSkill? softwareSkill)
    {
        if (softwareSkill == null)
            throw new BusinessException(SoftwareSkillsBusinessMessages.SoftwareSkillNotExists);
        return Task.CompletedTask;
    }

    public async Task SoftwareSkillIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        SoftwareSkill? softwareSkill = await _softwareSkillRepository.GetAsync(
            predicate: ss => ss.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SoftwareSkillShouldExistWhenSelected(softwareSkill);
    }
}
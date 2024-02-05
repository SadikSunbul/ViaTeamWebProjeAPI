using Application.Features.HeroSectionWrites.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.HeroSectionWrites.Rules;

public class HeroSectionWriteBusinessRules : BaseBusinessRules
{
    private readonly IHeroSectionWriteRepository _heroSectionWriteRepository;

    public HeroSectionWriteBusinessRules(IHeroSectionWriteRepository heroSectionWriteRepository)
    {
        _heroSectionWriteRepository = heroSectionWriteRepository;
    }

    public Task HeroSectionWriteShouldExistWhenSelected(HeroSectionWrite? heroSectionWrite)
    {
        if (heroSectionWrite == null)
            throw new BusinessException(HeroSectionWritesBusinessMessages.HeroSectionWriteNotExists);
        return Task.CompletedTask;
    }

    public async Task HeroSectionWriteIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        HeroSectionWrite? heroSectionWrite = await _heroSectionWriteRepository.GetAsync(
            predicate: hsw => hsw.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await HeroSectionWriteShouldExistWhenSelected(heroSectionWrite);
    }
}
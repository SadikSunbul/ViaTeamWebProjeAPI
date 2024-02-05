using Application.Features.FeaturedSectionEntities.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.FeaturedSectionEntities.Rules;

public class FeaturedSectionEntitieBusinessRules : BaseBusinessRules
{
    private readonly IFeaturedSectionEntitieRepository _featuredSectionEntitieRepository;

    public FeaturedSectionEntitieBusinessRules(IFeaturedSectionEntitieRepository featuredSectionEntitieRepository)
    {
        _featuredSectionEntitieRepository = featuredSectionEntitieRepository;
    }

    public Task FeaturedSectionEntitieShouldExistWhenSelected(FeaturedSectionEntitie? featuredSectionEntitie)
    {
        if (featuredSectionEntitie == null)
            throw new BusinessException(FeaturedSectionEntitiesBusinessMessages.FeaturedSectionEntitieNotExists);
        return Task.CompletedTask;
    }

    public async Task FeaturedSectionEntitieIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        FeaturedSectionEntitie? featuredSectionEntitie = await _featuredSectionEntitieRepository.GetAsync(
            predicate: fse => fse.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await FeaturedSectionEntitieShouldExistWhenSelected(featuredSectionEntitie);
    }
}
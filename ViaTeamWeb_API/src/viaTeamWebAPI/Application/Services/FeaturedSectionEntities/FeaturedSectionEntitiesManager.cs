using Application.Features.FeaturedSectionEntities.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.FeaturedSectionEntities;

public class FeaturedSectionEntitiesManager : IFeaturedSectionEntitiesService
{
    private readonly IFeaturedSectionEntitieRepository _featuredSectionEntitieRepository;
    private readonly FeaturedSectionEntitieBusinessRules _featuredSectionEntitieBusinessRules;

    public FeaturedSectionEntitiesManager(IFeaturedSectionEntitieRepository featuredSectionEntitieRepository, FeaturedSectionEntitieBusinessRules featuredSectionEntitieBusinessRules)
    {
        _featuredSectionEntitieRepository = featuredSectionEntitieRepository;
        _featuredSectionEntitieBusinessRules = featuredSectionEntitieBusinessRules;
    }

    public async Task<FeaturedSectionEntitie?> GetAsync(
        Expression<Func<FeaturedSectionEntitie, bool>> predicate,
        Func<IQueryable<FeaturedSectionEntitie>, IIncludableQueryable<FeaturedSectionEntitie, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        FeaturedSectionEntitie? featuredSectionEntitie = await _featuredSectionEntitieRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return featuredSectionEntitie;
    }

    public async Task<IPaginate<FeaturedSectionEntitie>?> GetListAsync(
        Expression<Func<FeaturedSectionEntitie, bool>>? predicate = null,
        Func<IQueryable<FeaturedSectionEntitie>, IOrderedQueryable<FeaturedSectionEntitie>>? orderBy = null,
        Func<IQueryable<FeaturedSectionEntitie>, IIncludableQueryable<FeaturedSectionEntitie, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<FeaturedSectionEntitie> featuredSectionEntitieList = await _featuredSectionEntitieRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return featuredSectionEntitieList;
    }

    public async Task<FeaturedSectionEntitie> AddAsync(FeaturedSectionEntitie featuredSectionEntitie)
    {
        FeaturedSectionEntitie addedFeaturedSectionEntitie = await _featuredSectionEntitieRepository.AddAsync(featuredSectionEntitie);

        return addedFeaturedSectionEntitie;
    }

    public async Task<FeaturedSectionEntitie> UpdateAsync(FeaturedSectionEntitie featuredSectionEntitie)
    {
        FeaturedSectionEntitie updatedFeaturedSectionEntitie = await _featuredSectionEntitieRepository.UpdateAsync(featuredSectionEntitie);

        return updatedFeaturedSectionEntitie;
    }

    public async Task<FeaturedSectionEntitie> DeleteAsync(FeaturedSectionEntitie featuredSectionEntitie, bool permanent = false)
    {
        FeaturedSectionEntitie deletedFeaturedSectionEntitie = await _featuredSectionEntitieRepository.DeleteAsync(featuredSectionEntitie);

        return deletedFeaturedSectionEntitie;
    }
}

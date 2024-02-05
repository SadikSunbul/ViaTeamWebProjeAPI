using Application.Features.HeroSectionWrites.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.HeroSectionWrites;

public class HeroSectionWritesManager : IHeroSectionWritesService
{
    private readonly IHeroSectionWriteRepository _heroSectionWriteRepository;
    private readonly HeroSectionWriteBusinessRules _heroSectionWriteBusinessRules;

    public HeroSectionWritesManager(IHeroSectionWriteRepository heroSectionWriteRepository, HeroSectionWriteBusinessRules heroSectionWriteBusinessRules)
    {
        _heroSectionWriteRepository = heroSectionWriteRepository;
        _heroSectionWriteBusinessRules = heroSectionWriteBusinessRules;
    }

    public async Task<HeroSectionWrite?> GetAsync(
        Expression<Func<HeroSectionWrite, bool>> predicate,
        Func<IQueryable<HeroSectionWrite>, IIncludableQueryable<HeroSectionWrite, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        HeroSectionWrite? heroSectionWrite = await _heroSectionWriteRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return heroSectionWrite;
    }

    public async Task<IPaginate<HeroSectionWrite>?> GetListAsync(
        Expression<Func<HeroSectionWrite, bool>>? predicate = null,
        Func<IQueryable<HeroSectionWrite>, IOrderedQueryable<HeroSectionWrite>>? orderBy = null,
        Func<IQueryable<HeroSectionWrite>, IIncludableQueryable<HeroSectionWrite, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<HeroSectionWrite> heroSectionWriteList = await _heroSectionWriteRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return heroSectionWriteList;
    }

    public async Task<HeroSectionWrite> AddAsync(HeroSectionWrite heroSectionWrite)
    {
        HeroSectionWrite addedHeroSectionWrite = await _heroSectionWriteRepository.AddAsync(heroSectionWrite);

        return addedHeroSectionWrite;
    }

    public async Task<HeroSectionWrite> UpdateAsync(HeroSectionWrite heroSectionWrite)
    {
        HeroSectionWrite updatedHeroSectionWrite = await _heroSectionWriteRepository.UpdateAsync(heroSectionWrite);

        return updatedHeroSectionWrite;
    }

    public async Task<HeroSectionWrite> DeleteAsync(HeroSectionWrite heroSectionWrite, bool permanent = false)
    {
        HeroSectionWrite deletedHeroSectionWrite = await _heroSectionWriteRepository.DeleteAsync(heroSectionWrite);

        return deletedHeroSectionWrite;
    }
}

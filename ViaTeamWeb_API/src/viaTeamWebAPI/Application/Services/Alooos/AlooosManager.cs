using Application.Features.Alooos.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Alooos;

public class AlooosManager : IAlooosService
{
    private readonly IAloooRepository _aloooRepository;
    private readonly AloooBusinessRules _aloooBusinessRules;

    public AlooosManager(IAloooRepository aloooRepository, AloooBusinessRules aloooBusinessRules)
    {
        _aloooRepository = aloooRepository;
        _aloooBusinessRules = aloooBusinessRules;
    }

    public async Task<Alooo?> GetAsync(
        Expression<Func<Alooo, bool>> predicate,
        Func<IQueryable<Alooo>, IIncludableQueryable<Alooo, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Alooo? alooo = await _aloooRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return alooo;
    }

    public async Task<IPaginate<Alooo>?> GetListAsync(
        Expression<Func<Alooo, bool>>? predicate = null,
        Func<IQueryable<Alooo>, IOrderedQueryable<Alooo>>? orderBy = null,
        Func<IQueryable<Alooo>, IIncludableQueryable<Alooo, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Alooo> aloooList = await _aloooRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return aloooList;
    }

    public async Task<Alooo> AddAsync(Alooo alooo)
    {
        Alooo addedAlooo = await _aloooRepository.AddAsync(alooo);

        return addedAlooo;
    }

    public async Task<Alooo> UpdateAsync(Alooo alooo)
    {
        Alooo updatedAlooo = await _aloooRepository.UpdateAsync(alooo);

        return updatedAlooo;
    }

    public async Task<Alooo> DeleteAsync(Alooo alooo, bool permanent = false)
    {
        Alooo deletedAlooo = await _aloooRepository.DeleteAsync(alooo);

        return deletedAlooo;
    }
}

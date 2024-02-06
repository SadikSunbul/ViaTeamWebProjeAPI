using Application.Features.ExternalLinks.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ExternalLinks;

public class ExternalLinksManager : IExternalLinksService
{
    private readonly IExternalLinkRepository _externalLinkRepository;
    private readonly ExternalLinkBusinessRules _externalLinkBusinessRules;

    public ExternalLinksManager(IExternalLinkRepository externalLinkRepository, ExternalLinkBusinessRules externalLinkBusinessRules)
    {
        _externalLinkRepository = externalLinkRepository;
        _externalLinkBusinessRules = externalLinkBusinessRules;
    }

    public async Task<ExternalLink?> GetAsync(
        Expression<Func<ExternalLink, bool>> predicate,
        Func<IQueryable<ExternalLink>, IIncludableQueryable<ExternalLink, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ExternalLink? externalLink = await _externalLinkRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return externalLink;
    }

    public async Task<IPaginate<ExternalLink>?> GetListAsync(
        Expression<Func<ExternalLink, bool>>? predicate = null,
        Func<IQueryable<ExternalLink>, IOrderedQueryable<ExternalLink>>? orderBy = null,
        Func<IQueryable<ExternalLink>, IIncludableQueryable<ExternalLink, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ExternalLink> externalLinkList = await _externalLinkRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return externalLinkList;
    }

    public async Task<ExternalLink> AddAsync(ExternalLink externalLink)
    {
        ExternalLink addedExternalLink = await _externalLinkRepository.AddAsync(externalLink);

        return addedExternalLink;
    }

    public async Task<ExternalLink> UpdateAsync(ExternalLink externalLink)
    {
        ExternalLink updatedExternalLink = await _externalLinkRepository.UpdateAsync(externalLink);

        return updatedExternalLink;
    }

    public async Task<ExternalLink> DeleteAsync(ExternalLink externalLink, bool permanent = false)
    {
        ExternalLink deletedExternalLink = await _externalLinkRepository.DeleteAsync(externalLink);

        return deletedExternalLink;
    }
}

using Application.Features.ExternalLinks.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ExternalLinks.Rules;

public class ExternalLinkBusinessRules : BaseBusinessRules
{
    private readonly IExternalLinkRepository _externalLinkRepository;

    public ExternalLinkBusinessRules(IExternalLinkRepository externalLinkRepository)
    {
        _externalLinkRepository = externalLinkRepository;
    }

    public Task ExternalLinkShouldExistWhenSelected(ExternalLink? externalLink)
    {
        if (externalLink == null)
            throw new BusinessException(ExternalLinksBusinessMessages.ExternalLinkNotExists);
        return Task.CompletedTask;
    }

    public async Task ExternalLinkIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ExternalLink? externalLink = await _externalLinkRepository.GetAsync(
            predicate: el => el.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ExternalLinkShouldExistWhenSelected(externalLink);
    }
}
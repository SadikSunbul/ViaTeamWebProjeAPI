using Application.Features.Denemes.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Denemes.Rules;

public class DenemeBusinessRules : BaseBusinessRules
{
    private readonly IDenemeRepository _denemeRepository;

    public DenemeBusinessRules(IDenemeRepository denemeRepository)
    {
        _denemeRepository = denemeRepository;
    }

    public Task DenemeShouldExistWhenSelected(Deneme? deneme)
    {
        if (deneme == null)
            throw new BusinessException(DenemesBusinessMessages.DenemeNotExists);
        return Task.CompletedTask;
    }

    public async Task DenemeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Deneme? deneme = await _denemeRepository.GetAsync(
            predicate: d => d.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DenemeShouldExistWhenSelected(deneme);
    }
}
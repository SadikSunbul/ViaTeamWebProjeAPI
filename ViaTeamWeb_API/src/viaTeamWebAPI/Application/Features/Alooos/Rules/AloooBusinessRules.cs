using Application.Features.Alooos.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Alooos.Rules;

public class AloooBusinessRules : BaseBusinessRules
{
    private readonly IAloooRepository _aloooRepository;

    public AloooBusinessRules(IAloooRepository aloooRepository)
    {
        _aloooRepository = aloooRepository;
    }

    public Task AloooShouldExistWhenSelected(Alooo? alooo)
    {
        if (alooo == null)
            throw new BusinessException(AlooosBusinessMessages.AloooNotExists);
        return Task.CompletedTask;
    }

    public async Task AloooIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Alooo? alooo = await _aloooRepository.GetAsync(
            predicate: a => a.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AloooShouldExistWhenSelected(alooo);
    }
}
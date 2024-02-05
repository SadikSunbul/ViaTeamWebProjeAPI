using Application.Features.BusinessAreas.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.BusinessAreas.Rules;

public class BusinessAreaBusinessRules : BaseBusinessRules
{
    private readonly IBusinessAreaRepository _businessAreaRepository;

    public BusinessAreaBusinessRules(IBusinessAreaRepository businessAreaRepository)
    {
        _businessAreaRepository = businessAreaRepository;
    }

    public Task BusinessAreaShouldExistWhenSelected(BusinessArea? businessArea)
    {
        if (businessArea == null)
            throw new BusinessException(BusinessAreasBusinessMessages.BusinessAreaNotExists);
        return Task.CompletedTask;
    }

    public async Task BusinessAreaIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        BusinessArea? businessArea = await _businessAreaRepository.GetAsync(
            predicate: ba => ba.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BusinessAreaShouldExistWhenSelected(businessArea);
    }
}
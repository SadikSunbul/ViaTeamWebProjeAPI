using Application.Features.BusinessAreas.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.BusinessAreas;

public class BusinessAreasManager : IBusinessAreasService
{
    private readonly IBusinessAreaRepository _businessAreaRepository;
    private readonly BusinessAreaBusinessRules _businessAreaBusinessRules;

    public BusinessAreasManager(IBusinessAreaRepository businessAreaRepository, BusinessAreaBusinessRules businessAreaBusinessRules)
    {
        _businessAreaRepository = businessAreaRepository;
        _businessAreaBusinessRules = businessAreaBusinessRules;
    }

    public async Task<BusinessArea?> GetAsync(
        Expression<Func<BusinessArea, bool>> predicate,
        Func<IQueryable<BusinessArea>, IIncludableQueryable<BusinessArea, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        BusinessArea? businessArea = await _businessAreaRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return businessArea;
    }

    public async Task<IPaginate<BusinessArea>?> GetListAsync(
        Expression<Func<BusinessArea, bool>>? predicate = null,
        Func<IQueryable<BusinessArea>, IOrderedQueryable<BusinessArea>>? orderBy = null,
        Func<IQueryable<BusinessArea>, IIncludableQueryable<BusinessArea, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<BusinessArea> businessAreaList = await _businessAreaRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return businessAreaList;
    }

    public async Task<BusinessArea> AddAsync(BusinessArea businessArea)
    {
        BusinessArea addedBusinessArea = await _businessAreaRepository.AddAsync(businessArea);

        return addedBusinessArea;
    }

    public async Task<BusinessArea> UpdateAsync(BusinessArea businessArea)
    {
        BusinessArea updatedBusinessArea = await _businessAreaRepository.UpdateAsync(businessArea);

        return updatedBusinessArea;
    }

    public async Task<BusinessArea> DeleteAsync(BusinessArea businessArea, bool permanent = false)
    {
        BusinessArea deletedBusinessArea = await _businessAreaRepository.DeleteAsync(businessArea);

        return deletedBusinessArea;
    }
}

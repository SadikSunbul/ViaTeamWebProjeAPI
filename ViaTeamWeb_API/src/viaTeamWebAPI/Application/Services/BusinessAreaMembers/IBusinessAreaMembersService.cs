using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.BusinessAreaMembers;

public interface IBusinessAreaMembersService
{
    Task<BusinessAreaMember?> GetAsync(
        Expression<Func<BusinessAreaMember, bool>> predicate,
        Func<IQueryable<BusinessAreaMember>, IIncludableQueryable<BusinessAreaMember, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<BusinessAreaMember>?> GetListAsync(
        Expression<Func<BusinessAreaMember, bool>>? predicate = null,
        Func<IQueryable<BusinessAreaMember>, IOrderedQueryable<BusinessAreaMember>>? orderBy = null,
        Func<IQueryable<BusinessAreaMember>, IIncludableQueryable<BusinessAreaMember, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<BusinessAreaMember> AddAsync(BusinessAreaMember businessAreaMember);
    Task<BusinessAreaMember> UpdateAsync(BusinessAreaMember businessAreaMember);
    Task<BusinessAreaMember> DeleteAsync(BusinessAreaMember businessAreaMember, bool permanent = false);
}

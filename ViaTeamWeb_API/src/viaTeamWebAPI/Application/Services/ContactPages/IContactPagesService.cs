using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContactPages;

public interface IContactPagesService
{
    Task<ContactPage?> GetAsync(
        Expression<Func<ContactPage, bool>> predicate,
        Func<IQueryable<ContactPage>, IIncludableQueryable<ContactPage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContactPage>?> GetListAsync(
        Expression<Func<ContactPage, bool>>? predicate = null,
        Func<IQueryable<ContactPage>, IOrderedQueryable<ContactPage>>? orderBy = null,
        Func<IQueryable<ContactPage>, IIncludableQueryable<ContactPage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContactPage> AddAsync(ContactPage contactPage);
    Task<ContactPage> UpdateAsync(ContactPage contactPage);
    Task<ContactPage> DeleteAsync(ContactPage contactPage, bool permanent = false);
}

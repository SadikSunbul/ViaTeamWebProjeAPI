using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Tests;

public interface ITestsService
{
    Task<Test?> GetAsync(
        Expression<Func<Test, bool>> predicate,
        Func<IQueryable<Test>, IIncludableQueryable<Test, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Test>?> GetListAsync(
        Expression<Func<Test, bool>>? predicate = null,
        Func<IQueryable<Test>, IOrderedQueryable<Test>>? orderBy = null,
        Func<IQueryable<Test>, IIncludableQueryable<Test, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Test> AddAsync(Test test);
    Task<Test> UpdateAsync(Test test);
    Task<Test> DeleteAsync(Test test, bool permanent = false);
}

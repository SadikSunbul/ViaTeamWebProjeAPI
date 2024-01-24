using Application.Features.Tests.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Tests;

public class TestsManager : ITestsService
{
    private readonly ITestRepository _testRepository;
    private readonly TestBusinessRules _testBusinessRules;

    public TestsManager(ITestRepository testRepository, TestBusinessRules testBusinessRules)
    {
        _testRepository = testRepository;
        _testBusinessRules = testBusinessRules;
    }

    public async Task<Test?> GetAsync(
        Expression<Func<Test, bool>> predicate,
        Func<IQueryable<Test>, IIncludableQueryable<Test, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Test? test = await _testRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return test;
    }

    public async Task<IPaginate<Test>?> GetListAsync(
        Expression<Func<Test, bool>>? predicate = null,
        Func<IQueryable<Test>, IOrderedQueryable<Test>>? orderBy = null,
        Func<IQueryable<Test>, IIncludableQueryable<Test, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Test> testList = await _testRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return testList;
    }

    public async Task<Test> AddAsync(Test test)
    {
        Test addedTest = await _testRepository.AddAsync(test);

        return addedTest;
    }

    public async Task<Test> UpdateAsync(Test test)
    {
        Test updatedTest = await _testRepository.UpdateAsync(test);

        return updatedTest;
    }

    public async Task<Test> DeleteAsync(Test test, bool permanent = false)
    {
        Test deletedTest = await _testRepository.DeleteAsync(test);

        return deletedTest;
    }
}

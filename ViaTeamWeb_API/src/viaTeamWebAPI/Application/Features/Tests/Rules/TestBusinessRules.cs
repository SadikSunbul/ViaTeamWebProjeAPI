using Application.Features.Tests.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Tests.Rules;

public class TestBusinessRules : BaseBusinessRules
{
    private readonly ITestRepository _testRepository;

    public TestBusinessRules(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public Task TestShouldExistWhenSelected(Test? test)
    {
        if (test == null)
            throw new BusinessException(TestsBusinessMessages.TestNotExists);
        return Task.CompletedTask;
    }

    public async Task TestIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Test? test = await _testRepository.GetAsync(
            predicate: t => t.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TestShouldExistWhenSelected(test);
    }
}
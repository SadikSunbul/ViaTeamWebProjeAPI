using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TestRepository : EfRepositoryBase<Test, Guid, BaseDbContext>, ITestRepository
{
    public TestRepository(BaseDbContext context) : base(context)
    {
    }
}
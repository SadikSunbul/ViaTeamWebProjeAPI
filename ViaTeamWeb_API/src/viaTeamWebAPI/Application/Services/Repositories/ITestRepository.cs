using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITestRepository : IAsyncRepository<Test, Guid>, IRepository<Test, Guid>
{
}
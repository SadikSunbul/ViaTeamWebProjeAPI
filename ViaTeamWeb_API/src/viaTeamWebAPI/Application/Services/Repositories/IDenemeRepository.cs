using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IDenemeRepository : IAsyncRepository<Deneme, Guid>, IRepository<Deneme, Guid>
{
}
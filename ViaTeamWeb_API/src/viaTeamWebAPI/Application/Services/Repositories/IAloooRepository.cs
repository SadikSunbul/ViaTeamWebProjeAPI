using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAloooRepository : IAsyncRepository<Alooo, Guid>, IRepository<Alooo, Guid>
{
}
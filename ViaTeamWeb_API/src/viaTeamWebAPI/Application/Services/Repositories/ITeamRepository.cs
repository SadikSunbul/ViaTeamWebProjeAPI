using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITeamRepository : IAsyncRepository<Team, Guid>, IRepository<Team, Guid>
{
}
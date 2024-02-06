using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITeamAboutRepository : IAsyncRepository<TeamAbout, Guid>, IRepository<TeamAbout, Guid>
{
}
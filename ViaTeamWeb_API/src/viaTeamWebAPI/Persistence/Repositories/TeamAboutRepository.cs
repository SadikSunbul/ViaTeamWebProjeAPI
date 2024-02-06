using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TeamAboutRepository : EfRepositoryBase<TeamAbout, Guid, BaseDbContext>, ITeamAboutRepository
{
    public TeamAboutRepository(BaseDbContext context) : base(context)
    {
    }
}
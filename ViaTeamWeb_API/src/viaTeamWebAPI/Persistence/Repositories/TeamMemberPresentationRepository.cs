using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TeamMemberPresentationRepository : EfRepositoryBase<TeamMemberPresentation, Guid, BaseDbContext>, ITeamMemberPresentationRepository
{
    public TeamMemberPresentationRepository(BaseDbContext context) : base(context)
    {
    }
}
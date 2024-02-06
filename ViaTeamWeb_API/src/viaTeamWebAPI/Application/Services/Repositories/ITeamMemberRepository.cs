using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITeamMemberRepository : IAsyncRepository<TeamMember, Guid>, IRepository<TeamMember, Guid>
{
}
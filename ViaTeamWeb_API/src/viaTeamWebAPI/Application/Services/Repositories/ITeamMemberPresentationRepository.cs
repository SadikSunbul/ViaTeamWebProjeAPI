using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITeamMemberPresentationRepository : IAsyncRepository<TeamMemberPresentation, Guid>, IRepository<TeamMemberPresentation, Guid>
{
}
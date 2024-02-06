using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ISoftwareSkillRepository : IAsyncRepository<SoftwareSkill, Guid>, IRepository<SoftwareSkill, Guid>
{
}
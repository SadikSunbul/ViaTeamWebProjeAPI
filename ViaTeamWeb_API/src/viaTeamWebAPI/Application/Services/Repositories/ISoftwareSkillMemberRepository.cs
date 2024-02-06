using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ISoftwareSkillMemberRepository : IAsyncRepository<SoftwareSkillMember, Guid>, IRepository<SoftwareSkillMember, Guid>
{
}
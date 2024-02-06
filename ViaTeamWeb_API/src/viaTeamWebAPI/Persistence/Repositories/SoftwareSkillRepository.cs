using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SoftwareSkillRepository : EfRepositoryBase<SoftwareSkill, Guid, BaseDbContext>, ISoftwareSkillRepository
{
    public SoftwareSkillRepository(BaseDbContext context) : base(context)
    {
    }
}
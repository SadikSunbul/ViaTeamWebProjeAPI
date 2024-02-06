using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SoftwareSkillMemberRepository : EfRepositoryBase<SoftwareSkillMember, Guid, BaseDbContext>, ISoftwareSkillMemberRepository
{
    public SoftwareSkillMemberRepository(BaseDbContext context) : base(context)
    {
    }
}
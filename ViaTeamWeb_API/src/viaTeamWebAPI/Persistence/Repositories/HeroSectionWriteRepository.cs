using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class HeroSectionWriteRepository : EfRepositoryBase<HeroSectionWrite, Guid, BaseDbContext>, IHeroSectionWriteRepository
{
    public HeroSectionWriteRepository(BaseDbContext context) : base(context)
    {
    }
}
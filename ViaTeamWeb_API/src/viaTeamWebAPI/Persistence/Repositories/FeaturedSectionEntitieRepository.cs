using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FeaturedSectionEntitieRepository : EfRepositoryBase<FeaturedSectionEntitie, Guid, BaseDbContext>, IFeaturedSectionEntitieRepository
{
    public FeaturedSectionEntitieRepository(BaseDbContext context) : base(context)
    {
    }
}
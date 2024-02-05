using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FeaturedArticleCardRepository : EfRepositoryBase<FeaturedArticleCard, Guid, BaseDbContext>, IFeaturedArticleCardRepository
{
    public FeaturedArticleCardRepository(BaseDbContext context) : base(context)
    {
    }
}
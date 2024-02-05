using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IFeaturedArticleCardRepository : IAsyncRepository<FeaturedArticleCard, Guid>, IRepository<FeaturedArticleCard, Guid>
{
}
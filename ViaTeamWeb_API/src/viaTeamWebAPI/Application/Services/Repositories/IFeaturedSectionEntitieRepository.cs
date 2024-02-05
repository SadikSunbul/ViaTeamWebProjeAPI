using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IFeaturedSectionEntitieRepository : IAsyncRepository<FeaturedSectionEntitie, Guid>, IRepository<FeaturedSectionEntitie, Guid>
{
}
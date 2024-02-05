using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IHeroSectionWriteRepository : IAsyncRepository<HeroSectionWrite, Guid>, IRepository<HeroSectionWrite, Guid>
{
}
using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IExternalLinkRepository : IAsyncRepository<ExternalLink, Guid>, IRepository<ExternalLink, Guid>
{
}
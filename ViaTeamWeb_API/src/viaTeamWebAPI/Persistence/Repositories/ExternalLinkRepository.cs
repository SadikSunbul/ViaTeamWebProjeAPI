using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ExternalLinkRepository : EfRepositoryBase<ExternalLink, Guid, BaseDbContext>, IExternalLinkRepository
{
    public ExternalLinkRepository(BaseDbContext context) : base(context)
    {
    }
}
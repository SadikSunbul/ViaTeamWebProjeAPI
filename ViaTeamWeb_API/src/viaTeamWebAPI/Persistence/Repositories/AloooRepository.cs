using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AloooRepository : EfRepositoryBase<Alooo, Guid, BaseDbContext>, IAloooRepository
{
    public AloooRepository(BaseDbContext context) : base(context)
    {
    }
}
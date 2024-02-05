using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BusinessAreaRepository : EfRepositoryBase<BusinessArea, Guid, BaseDbContext>, IBusinessAreaRepository
{
    public BusinessAreaRepository(BaseDbContext context) : base(context)
    {
    }
}
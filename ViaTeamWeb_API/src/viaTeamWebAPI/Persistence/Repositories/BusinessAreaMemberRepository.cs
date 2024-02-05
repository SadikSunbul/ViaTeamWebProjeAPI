using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BusinessAreaMemberRepository : EfRepositoryBase<BusinessAreaMember, Guid, BaseDbContext>, IBusinessAreaMemberRepository
{
    public BusinessAreaMemberRepository(BaseDbContext context) : base(context)
    {
    }
}
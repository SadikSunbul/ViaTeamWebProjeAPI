using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContactPageRepository : EfRepositoryBase<ContactPage, Guid, BaseDbContext>, IContactPageRepository
{
    public ContactPageRepository(BaseDbContext context) : base(context)
    {
    }
}
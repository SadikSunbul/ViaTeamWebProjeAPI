using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContactPageRepository : IAsyncRepository<ContactPage, Guid>, IRepository<ContactPage, Guid>
{
}
using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IBusinessAreaRepository : IAsyncRepository<BusinessArea, Guid>, IRepository<BusinessArea, Guid>
{
}
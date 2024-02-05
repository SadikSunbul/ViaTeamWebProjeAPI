using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IBusinessAreaMemberRepository : IAsyncRepository<BusinessAreaMember, Guid>, IRepository<BusinessAreaMember, Guid>
{
}
using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.BusinessAreaMembers.Queries.GetById;

public class GetByIdBusinessAreaMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BusinessAreaId { get; set; }
    public Guid MemberId { get; set; }
   
}
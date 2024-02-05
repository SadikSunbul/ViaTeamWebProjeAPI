using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.BusinessAreaMembers.Queries.GetList;

public class GetListBusinessAreaMemberListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BusinessAreaId { get; set; }
    public Guid MemberId { get; set; }
    
}
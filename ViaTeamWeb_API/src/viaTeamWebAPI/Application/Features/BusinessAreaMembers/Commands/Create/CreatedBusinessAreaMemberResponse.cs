using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.BusinessAreaMembers.Commands.Create;

public class CreatedBusinessAreaMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BusinessAreaId { get; set; }
    public Guid MemberId { get; set; }
    
}
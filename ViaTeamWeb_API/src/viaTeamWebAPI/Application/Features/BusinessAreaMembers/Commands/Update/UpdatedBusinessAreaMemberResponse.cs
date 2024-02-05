using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.BusinessAreaMembers.Commands.Update;

public class UpdatedBusinessAreaMemberResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BusinessAreaId { get; set; }
    public Guid MemberId { get; set; }

}
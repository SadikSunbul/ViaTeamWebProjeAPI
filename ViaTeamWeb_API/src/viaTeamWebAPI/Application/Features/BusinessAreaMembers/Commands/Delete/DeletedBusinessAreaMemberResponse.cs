using Core.Application.Responses;

namespace Application.Features.BusinessAreaMembers.Commands.Delete;

public class DeletedBusinessAreaMemberResponse : IResponse
{
    public Guid Id { get; set; }
}
using Core.Application.Responses;

namespace Application.Features.ContactPages.Commands.Delete;

public class DeletedContactPageResponse : IResponse
{
    public Guid Id { get; set; }
}
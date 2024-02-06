using Core.Application.Responses;

namespace Application.Features.ExternalLinks.Commands.Delete;

public class DeletedExternalLinkResponse : IResponse
{
    public Guid Id { get; set; }
}
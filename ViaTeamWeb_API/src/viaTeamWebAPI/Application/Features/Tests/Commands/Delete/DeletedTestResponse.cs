using Core.Application.Responses;

namespace Application.Features.Tests.Commands.Delete;

public class DeletedTestResponse : IResponse
{
    public Guid Id { get; set; }
}
using Core.Application.Responses;

namespace Application.Features.Tests.Commands.Update;

public class UpdatedTestResponse : IResponse
{
    public Guid Id { get; set; }
    public string NAME { get; set; }
}
using Core.Application.Responses;

namespace Application.Features.Tests.Commands.Create;

public class CreatedTestResponse : IResponse
{
    public Guid Id { get; set; }
    public string NAME { get; set; }
}
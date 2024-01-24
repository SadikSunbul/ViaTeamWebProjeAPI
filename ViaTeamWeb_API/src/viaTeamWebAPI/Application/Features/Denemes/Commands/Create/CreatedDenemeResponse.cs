using Core.Application.Responses;

namespace Application.Features.Denemes.Commands.Create;

public class CreatedDenemeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
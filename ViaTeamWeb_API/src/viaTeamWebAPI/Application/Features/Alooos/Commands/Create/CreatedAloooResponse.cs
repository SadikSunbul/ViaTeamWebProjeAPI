using Core.Application.Responses;

namespace Application.Features.Alooos.Commands.Create;

public class CreatedAloooResponse : IResponse
{
    public Guid Id { get; set; }
    public string Deneme { get; set; }
}
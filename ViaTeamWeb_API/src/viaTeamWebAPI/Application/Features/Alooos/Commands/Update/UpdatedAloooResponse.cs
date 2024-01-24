using Core.Application.Responses;

namespace Application.Features.Alooos.Commands.Update;

public class UpdatedAloooResponse : IResponse
{
    public Guid Id { get; set; }
    public string Deneme { get; set; }
}
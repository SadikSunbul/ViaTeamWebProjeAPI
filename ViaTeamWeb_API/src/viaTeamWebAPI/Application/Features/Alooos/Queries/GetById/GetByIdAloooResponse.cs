using Core.Application.Responses;

namespace Application.Features.Alooos.Queries.GetById;

public class GetByIdAloooResponse : IResponse
{
    public Guid Id { get; set; }
    public string Deneme { get; set; }
}
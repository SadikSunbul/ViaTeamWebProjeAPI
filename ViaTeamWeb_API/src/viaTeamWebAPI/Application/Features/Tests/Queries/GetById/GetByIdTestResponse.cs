using Core.Application.Responses;

namespace Application.Features.Tests.Queries.GetById;

public class GetByIdTestResponse : IResponse
{
    public Guid Id { get; set; }
    public string NAME { get; set; }
}
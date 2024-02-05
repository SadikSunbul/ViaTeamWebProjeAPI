using Core.Application.Responses;

namespace Application.Features.BusinessAreas.Queries.GetById;

public class GetByIdBusinessAreaResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid MemberId { get; set; }
}
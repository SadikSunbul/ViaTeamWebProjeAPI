using Core.Application.Responses;

namespace Application.Features.BusinessAreas.Commands.Create;

public class CreatedBusinessAreaResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid MemberId { get; set; }
}
using Core.Application.Responses;

namespace Application.Features.BusinessAreas.Commands.Update;

public class UpdatedBusinessAreaResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid MemberId { get; set; }
}
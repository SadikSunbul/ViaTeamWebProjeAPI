using Core.Application.Dtos;

namespace Application.Features.BusinessAreas.Queries.GetList;

public class GetListBusinessAreaListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid MemberId { get; set; }
}
using Core.Application.Dtos;

namespace Application.Features.Denemes.Queries.GetList;

public class GetListDenemeListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
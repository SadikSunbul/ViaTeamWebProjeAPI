using Core.Application.Dtos;

namespace Application.Features.Alooos.Queries.GetList;

public class GetListAloooListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Deneme { get; set; }
}
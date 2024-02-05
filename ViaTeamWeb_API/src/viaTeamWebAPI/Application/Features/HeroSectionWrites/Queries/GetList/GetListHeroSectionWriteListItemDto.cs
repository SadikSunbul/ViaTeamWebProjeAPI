using Core.Application.Dtos;

namespace Application.Features.HeroSectionWrites.Queries.GetList;

public class GetListHeroSectionWriteListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Paragraph { get; set; }
    public string ButtonText { get; set; }
}
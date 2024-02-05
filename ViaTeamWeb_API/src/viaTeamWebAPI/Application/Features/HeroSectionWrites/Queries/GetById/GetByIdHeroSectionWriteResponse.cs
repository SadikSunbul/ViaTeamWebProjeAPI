using Core.Application.Responses;

namespace Application.Features.HeroSectionWrites.Queries.GetById;

public class GetByIdHeroSectionWriteResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Paragraph { get; set; }
    public string ButtonText { get; set; }
}
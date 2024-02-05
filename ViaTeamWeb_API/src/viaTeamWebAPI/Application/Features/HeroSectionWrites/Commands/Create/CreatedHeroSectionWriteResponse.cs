using Core.Application.Responses;

namespace Application.Features.HeroSectionWrites.Commands.Create;

public class CreatedHeroSectionWriteResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Paragraph { get; set; }
    public string ButtonText { get; set; }
}
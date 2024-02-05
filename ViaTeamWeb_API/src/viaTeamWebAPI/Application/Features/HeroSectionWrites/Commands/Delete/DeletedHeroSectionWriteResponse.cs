using Core.Application.Responses;

namespace Application.Features.HeroSectionWrites.Commands.Delete;

public class DeletedHeroSectionWriteResponse : IResponse
{
    public Guid Id { get; set; }
}
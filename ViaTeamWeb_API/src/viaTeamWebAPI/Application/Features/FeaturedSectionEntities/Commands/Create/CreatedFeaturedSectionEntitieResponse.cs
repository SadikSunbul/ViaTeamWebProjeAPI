using Core.Application.Responses;

namespace Application.Features.FeaturedSectionEntities.Commands.Create;

public class CreatedFeaturedSectionEntitieResponse : IResponse
{
    public Guid Id { get; set; }
    public string SmallTitle { get; set; }
    public string Title { get; set; }
}
using Core.Application.Responses;

namespace Application.Features.FeaturedSectionEntities.Queries.GetById;

public class GetByIdFeaturedSectionEntitieResponse : IResponse
{
    public Guid Id { get; set; }
    public string SmallTitle { get; set; }
    public string Title { get; set; }
}
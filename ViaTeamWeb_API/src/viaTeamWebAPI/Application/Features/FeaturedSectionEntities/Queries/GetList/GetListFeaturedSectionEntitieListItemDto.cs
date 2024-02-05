using Core.Application.Dtos;

namespace Application.Features.FeaturedSectionEntities.Queries.GetList;

public class GetListFeaturedSectionEntitieListItemDto : IDto
{
    public Guid Id { get; set; }
    public string SmallTitle { get; set; }
    public string Title { get; set; }
}
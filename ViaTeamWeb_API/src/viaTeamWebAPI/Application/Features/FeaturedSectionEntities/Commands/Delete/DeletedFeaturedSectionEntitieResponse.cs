using Core.Application.Responses;

namespace Application.Features.FeaturedSectionEntities.Commands.Delete;

public class DeletedFeaturedSectionEntitieResponse : IResponse
{
    public Guid Id { get; set; }
}
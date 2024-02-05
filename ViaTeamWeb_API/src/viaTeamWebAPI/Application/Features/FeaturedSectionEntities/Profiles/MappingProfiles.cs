using Application.Features.FeaturedSectionEntities.Commands.Create;
using Application.Features.FeaturedSectionEntities.Commands.Delete;
using Application.Features.FeaturedSectionEntities.Commands.Update;
using Application.Features.FeaturedSectionEntities.Queries.GetById;
using Application.Features.FeaturedSectionEntities.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.FeaturedSectionEntities.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<FeaturedSectionEntitie, CreateFeaturedSectionEntitieCommand>().ReverseMap();
        CreateMap<FeaturedSectionEntitie, CreatedFeaturedSectionEntitieResponse>().ReverseMap();
        CreateMap<FeaturedSectionEntitie, UpdateFeaturedSectionEntitieCommand>().ReverseMap();
        CreateMap<FeaturedSectionEntitie, UpdatedFeaturedSectionEntitieResponse>().ReverseMap();
        CreateMap<FeaturedSectionEntitie, DeleteFeaturedSectionEntitieCommand>().ReverseMap();
        CreateMap<FeaturedSectionEntitie, DeletedFeaturedSectionEntitieResponse>().ReverseMap();
        CreateMap<FeaturedSectionEntitie, GetByIdFeaturedSectionEntitieResponse>().ReverseMap();
        CreateMap<FeaturedSectionEntitie, GetListFeaturedSectionEntitieListItemDto>().ReverseMap();
        CreateMap<IPaginate<FeaturedSectionEntitie>, GetListResponse<GetListFeaturedSectionEntitieListItemDto>>().ReverseMap();
    }
}
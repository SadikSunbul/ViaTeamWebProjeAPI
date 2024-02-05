using Application.Features.FeaturedArticleCards.Commands.Create;
using Application.Features.FeaturedArticleCards.Commands.Delete;
using Application.Features.FeaturedArticleCards.Commands.Update;
using Application.Features.FeaturedArticleCards.Queries.GetById;
using Application.Features.FeaturedArticleCards.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.FeaturedArticleCards.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<FeaturedArticleCard, CreateFeaturedArticleCardCommand>().ReverseMap();
        CreateMap<FeaturedArticleCard, CreatedFeaturedArticleCardResponse>().ReverseMap();
        CreateMap<FeaturedArticleCard, UpdateFeaturedArticleCardCommand>().ReverseMap();
        CreateMap<FeaturedArticleCard, UpdatedFeaturedArticleCardResponse>().ReverseMap();
        CreateMap<FeaturedArticleCard, DeleteFeaturedArticleCardCommand>().ReverseMap();
        CreateMap<FeaturedArticleCard, DeletedFeaturedArticleCardResponse>().ReverseMap();
        CreateMap<FeaturedArticleCard, GetByIdFeaturedArticleCardResponse>().ReverseMap();
        CreateMap<FeaturedArticleCard, GetListFeaturedArticleCardListItemDto>().ReverseMap();
        CreateMap<IPaginate<FeaturedArticleCard>, GetListResponse<GetListFeaturedArticleCardListItemDto>>().ReverseMap();
    }
}
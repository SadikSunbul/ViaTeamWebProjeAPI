using Application.Features.HeroSectionWrites.Commands.Create;
using Application.Features.HeroSectionWrites.Commands.Delete;
using Application.Features.HeroSectionWrites.Commands.Update;
using Application.Features.HeroSectionWrites.Queries.GetById;
using Application.Features.HeroSectionWrites.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.HeroSectionWrites.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<HeroSectionWrite, CreateHeroSectionWriteCommand>().ReverseMap();
        CreateMap<HeroSectionWrite, CreatedHeroSectionWriteResponse>().ReverseMap();
        CreateMap<HeroSectionWrite, UpdateHeroSectionWriteCommand>().ReverseMap();
        CreateMap<HeroSectionWrite, UpdatedHeroSectionWriteResponse>().ReverseMap();
        CreateMap<HeroSectionWrite, DeleteHeroSectionWriteCommand>().ReverseMap();
        CreateMap<HeroSectionWrite, DeletedHeroSectionWriteResponse>().ReverseMap();
        CreateMap<HeroSectionWrite, GetByIdHeroSectionWriteResponse>().ReverseMap();
        CreateMap<HeroSectionWrite, GetListHeroSectionWriteListItemDto>().ReverseMap();
        CreateMap<IPaginate<HeroSectionWrite>, GetListResponse<GetListHeroSectionWriteListItemDto>>().ReverseMap();
    }
}
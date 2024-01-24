using Application.Features.Alooos.Commands.Create;
using Application.Features.Alooos.Commands.Delete;
using Application.Features.Alooos.Commands.Update;
using Application.Features.Alooos.Queries.GetById;
using Application.Features.Alooos.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Alooos.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Alooo, CreateAloooCommand>().ReverseMap();
        CreateMap<Alooo, CreatedAloooResponse>().ReverseMap();
        CreateMap<Alooo, UpdateAloooCommand>().ReverseMap();
        CreateMap<Alooo, UpdatedAloooResponse>().ReverseMap();
        CreateMap<Alooo, DeleteAloooCommand>().ReverseMap();
        CreateMap<Alooo, DeletedAloooResponse>().ReverseMap();
        CreateMap<Alooo, GetByIdAloooResponse>().ReverseMap();
        CreateMap<Alooo, GetListAloooListItemDto>().ReverseMap();
        CreateMap<IPaginate<Alooo>, GetListResponse<GetListAloooListItemDto>>().ReverseMap();
    }
}
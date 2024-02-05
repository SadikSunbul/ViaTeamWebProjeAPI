using Application.Features.BusinessAreas.Commands.Create;
using Application.Features.BusinessAreas.Commands.Delete;
using Application.Features.BusinessAreas.Commands.Update;
using Application.Features.BusinessAreas.Queries.GetById;
using Application.Features.BusinessAreas.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.BusinessAreas.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<BusinessArea, CreateBusinessAreaCommand>().ReverseMap();
        CreateMap<BusinessArea, CreatedBusinessAreaResponse>().ReverseMap();
        CreateMap<BusinessArea, UpdateBusinessAreaCommand>().ReverseMap();
        CreateMap<BusinessArea, UpdatedBusinessAreaResponse>().ReverseMap();
        CreateMap<BusinessArea, DeleteBusinessAreaCommand>().ReverseMap();
        CreateMap<BusinessArea, DeletedBusinessAreaResponse>().ReverseMap();
        CreateMap<BusinessArea, GetByIdBusinessAreaResponse>().ReverseMap();
        CreateMap<BusinessArea, GetListBusinessAreaListItemDto>().ReverseMap();
        CreateMap<IPaginate<BusinessArea>, GetListResponse<GetListBusinessAreaListItemDto>>().ReverseMap();
    }
}
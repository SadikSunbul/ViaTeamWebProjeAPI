using Application.Features.ContactPages.Commands.Create;
using Application.Features.ContactPages.Commands.Delete;
using Application.Features.ContactPages.Commands.Update;
using Application.Features.ContactPages.Queries.GetById;
using Application.Features.ContactPages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContactPages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ContactPage, CreateContactPageCommand>().ReverseMap();
        CreateMap<ContactPage, CreatedContactPageResponse>().ReverseMap();
        CreateMap<ContactPage, UpdateContactPageCommand>().ReverseMap();
        CreateMap<ContactPage, UpdatedContactPageResponse>().ReverseMap();
        CreateMap<ContactPage, DeleteContactPageCommand>().ReverseMap();
        CreateMap<ContactPage, DeletedContactPageResponse>().ReverseMap();
        CreateMap<ContactPage, GetByIdContactPageResponse>().ReverseMap();
        CreateMap<ContactPage, GetListContactPageListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContactPage>, GetListResponse<GetListContactPageListItemDto>>().ReverseMap();
    }
}
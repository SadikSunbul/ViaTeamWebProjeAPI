using Application.Features.BusinessAreaMembers.Commands.Create;
using Application.Features.BusinessAreaMembers.Commands.Delete;
using Application.Features.BusinessAreaMembers.Commands.Update;
using Application.Features.BusinessAreaMembers.Queries.GetById;
using Application.Features.BusinessAreaMembers.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.BusinessAreaMembers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<BusinessAreaMember, CreateBusinessAreaMemberCommand>().ReverseMap();
        CreateMap<BusinessAreaMember, CreatedBusinessAreaMemberResponse>().ReverseMap();
        CreateMap<BusinessAreaMember, UpdateBusinessAreaMemberCommand>().ReverseMap();
        CreateMap<BusinessAreaMember, UpdatedBusinessAreaMemberResponse>().ReverseMap();
        CreateMap<BusinessAreaMember, DeleteBusinessAreaMemberCommand>().ReverseMap();
        CreateMap<BusinessAreaMember, DeletedBusinessAreaMemberResponse>().ReverseMap();
        CreateMap<BusinessAreaMember, GetByIdBusinessAreaMemberResponse>().ReverseMap();
        CreateMap<BusinessAreaMember, GetListBusinessAreaMemberListItemDto>().ReverseMap();
        CreateMap<IPaginate<BusinessAreaMember>, GetListResponse<GetListBusinessAreaMemberListItemDto>>().ReverseMap();
    }
}
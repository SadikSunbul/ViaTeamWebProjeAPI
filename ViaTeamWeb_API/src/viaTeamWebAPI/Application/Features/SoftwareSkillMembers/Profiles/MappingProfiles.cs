using Application.Features.SoftwareSkillMembers.Commands.Create;
using Application.Features.SoftwareSkillMembers.Commands.Delete;
using Application.Features.SoftwareSkillMembers.Commands.Update;
using Application.Features.SoftwareSkillMembers.Queries.GetById;
using Application.Features.SoftwareSkillMembers.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.SoftwareSkillMembers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SoftwareSkillMember, CreateSoftwareSkillMemberCommand>().ReverseMap();
        CreateMap<SoftwareSkillMember, CreatedSoftwareSkillMemberResponse>().ReverseMap();
        CreateMap<SoftwareSkillMember, UpdateSoftwareSkillMemberCommand>().ReverseMap();
        CreateMap<SoftwareSkillMember, UpdatedSoftwareSkillMemberResponse>().ReverseMap();
        CreateMap<SoftwareSkillMember, DeleteSoftwareSkillMemberCommand>().ReverseMap();
        CreateMap<SoftwareSkillMember, DeletedSoftwareSkillMemberResponse>().ReverseMap();
        CreateMap<SoftwareSkillMember, GetByIdSoftwareSkillMemberResponse>().ReverseMap();
        CreateMap<SoftwareSkillMember, GetListSoftwareSkillMemberListItemDto>().ReverseMap();
        CreateMap<IPaginate<SoftwareSkillMember>, GetListResponse<GetListSoftwareSkillMemberListItemDto>>().ReverseMap();
    }
}
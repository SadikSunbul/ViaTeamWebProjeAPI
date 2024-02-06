using Application.Features.SoftwareSkills.Commands.Create;
using Application.Features.SoftwareSkills.Commands.Delete;
using Application.Features.SoftwareSkills.Commands.Update;
using Application.Features.SoftwareSkills.Queries.GetById;
using Application.Features.SoftwareSkills.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.SoftwareSkills.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SoftwareSkill, CreateSoftwareSkillCommand>().ReverseMap();
        CreateMap<SoftwareSkill, CreatedSoftwareSkillResponse>().ReverseMap();
        CreateMap<SoftwareSkill, UpdateSoftwareSkillCommand>().ReverseMap();
        CreateMap<SoftwareSkill, UpdatedSoftwareSkillResponse>().ReverseMap();
        CreateMap<SoftwareSkill, DeleteSoftwareSkillCommand>().ReverseMap();
        CreateMap<SoftwareSkill, DeletedSoftwareSkillResponse>().ReverseMap();
        CreateMap<SoftwareSkill, GetByIdSoftwareSkillResponse>().ReverseMap();
        CreateMap<SoftwareSkill, GetListSoftwareSkillListItemDto>().ReverseMap();
        CreateMap<IPaginate<SoftwareSkill>, GetListResponse<GetListSoftwareSkillListItemDto>>().ReverseMap();
    }
}
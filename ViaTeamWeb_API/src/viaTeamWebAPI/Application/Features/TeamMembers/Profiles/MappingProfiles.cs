using Application.Features.TeamMembers.Commands.Create;
using Application.Features.TeamMembers.Commands.Delete;
using Application.Features.TeamMembers.Commands.Update;
using Application.Features.TeamMembers.Queries.GetById;
using Application.Features.TeamMembers.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.TeamMembers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<TeamMember, CreateTeamMemberCommand>().ReverseMap();
        CreateMap<TeamMember, CreatedTeamMemberResponse>().ReverseMap();
        CreateMap<TeamMember, UpdateTeamMemberCommand>().ReverseMap();
        CreateMap<TeamMember, UpdatedTeamMemberResponse>().ReverseMap();
        CreateMap<TeamMember, DeleteTeamMemberCommand>().ReverseMap();
        CreateMap<TeamMember, DeletedTeamMemberResponse>().ReverseMap();
        CreateMap<TeamMember, GetByIdTeamMemberResponse>().ReverseMap();
        CreateMap<TeamMember, GetListTeamMemberListItemDto>().ReverseMap();
        CreateMap<IPaginate<TeamMember>, GetListResponse<GetListTeamMemberListItemDto>>().ReverseMap();
    }
}
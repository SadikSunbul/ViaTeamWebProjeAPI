using Application.Features.TeamMemberPresentations.Commands.Create;
using Application.Features.TeamMemberPresentations.Commands.Delete;
using Application.Features.TeamMemberPresentations.Commands.Update;
using Application.Features.TeamMemberPresentations.Queries.GetById;
using Application.Features.TeamMemberPresentations.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.TeamMemberPresentations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<TeamMemberPresentation, CreateTeamMemberPresentationCommand>().ReverseMap();
        CreateMap<TeamMemberPresentation, CreatedTeamMemberPresentationResponse>().ReverseMap();
        CreateMap<TeamMemberPresentation, UpdateTeamMemberPresentationCommand>().ReverseMap();
        CreateMap<TeamMemberPresentation, UpdatedTeamMemberPresentationResponse>().ReverseMap();
        CreateMap<TeamMemberPresentation, DeleteTeamMemberPresentationCommand>().ReverseMap();
        CreateMap<TeamMemberPresentation, DeletedTeamMemberPresentationResponse>().ReverseMap();
        CreateMap<TeamMemberPresentation, GetByIdTeamMemberPresentationResponse>().ReverseMap();
        CreateMap<TeamMemberPresentation, GetListTeamMemberPresentationListItemDto>().ReverseMap();
        CreateMap<IPaginate<TeamMemberPresentation>, GetListResponse<GetListTeamMemberPresentationListItemDto>>().ReverseMap();
    }
}
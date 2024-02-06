using Application.Features.TeamAbouts.Commands.Create;
using Application.Features.TeamAbouts.Commands.Delete;
using Application.Features.TeamAbouts.Commands.Update;
using Application.Features.TeamAbouts.Queries.GetById;
using Application.Features.TeamAbouts.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.TeamAbouts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<TeamAbout, CreateTeamAboutCommand>().ReverseMap();
        CreateMap<TeamAbout, CreatedTeamAboutResponse>().ReverseMap();
        CreateMap<TeamAbout, UpdateTeamAboutCommand>().ReverseMap();
        CreateMap<TeamAbout, UpdatedTeamAboutResponse>().ReverseMap();
        CreateMap<TeamAbout, DeleteTeamAboutCommand>().ReverseMap();
        CreateMap<TeamAbout, DeletedTeamAboutResponse>().ReverseMap();
        CreateMap<TeamAbout, GetByIdTeamAboutResponse>().ReverseMap();
        CreateMap<TeamAbout, GetListTeamAboutListItemDto>().ReverseMap();
        CreateMap<IPaginate<TeamAbout>, GetListResponse<GetListTeamAboutListItemDto>>().ReverseMap();
    }
}
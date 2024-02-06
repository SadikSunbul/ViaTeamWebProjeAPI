using Application.Features.ExternalLinks.Commands.Create;
using Application.Features.ExternalLinks.Commands.Delete;
using Application.Features.ExternalLinks.Commands.Update;
using Application.Features.ExternalLinks.Queries.GetById;
using Application.Features.ExternalLinks.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ExternalLinks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ExternalLink, CreateExternalLinkCommand>().ReverseMap();
        CreateMap<ExternalLink, CreatedExternalLinkResponse>().ReverseMap();
        CreateMap<ExternalLink, UpdateExternalLinkCommand>().ReverseMap();
        CreateMap<ExternalLink, UpdatedExternalLinkResponse>().ReverseMap();
        CreateMap<ExternalLink, DeleteExternalLinkCommand>().ReverseMap();
        CreateMap<ExternalLink, DeletedExternalLinkResponse>().ReverseMap();
        CreateMap<ExternalLink, GetByIdExternalLinkResponse>().ReverseMap();
        CreateMap<ExternalLink, GetListExternalLinkListItemDto>().ReverseMap();
        CreateMap<IPaginate<ExternalLink>, GetListResponse<GetListExternalLinkListItemDto>>().ReverseMap();
    }
}
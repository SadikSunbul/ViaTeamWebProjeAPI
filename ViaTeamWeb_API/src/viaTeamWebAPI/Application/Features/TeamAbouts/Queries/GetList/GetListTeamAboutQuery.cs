using Application.Features.TeamAbouts.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.TeamAbouts.Constants.TeamAboutsOperationClaims;

namespace Application.Features.TeamAbouts.Queries.GetList;

public class GetListTeamAboutQuery : IRequest<GetListResponse<GetListTeamAboutListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    // public string[] Roles => new[] { Admin, Read };

    public class GetListTeamAboutQueryHandler : IRequestHandler<GetListTeamAboutQuery, GetListResponse<GetListTeamAboutListItemDto>>
    {
        private readonly ITeamAboutRepository _teamAboutRepository;
        private readonly IMapper _mapper;

        public GetListTeamAboutQueryHandler(ITeamAboutRepository teamAboutRepository, IMapper mapper)
        {
            _teamAboutRepository = teamAboutRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTeamAboutListItemDto>> Handle(GetListTeamAboutQuery request, CancellationToken cancellationToken)
        {
            IPaginate<TeamAbout> teamAbouts = await _teamAboutRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTeamAboutListItemDto> response = _mapper.Map<GetListResponse<GetListTeamAboutListItemDto>>(teamAbouts);
            return response;
        }
    }
}
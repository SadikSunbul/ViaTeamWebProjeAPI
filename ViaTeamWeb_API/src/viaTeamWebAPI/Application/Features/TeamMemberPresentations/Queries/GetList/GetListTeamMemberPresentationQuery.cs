using Application.Features.TeamMemberPresentations.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.TeamMemberPresentations.Constants.TeamMemberPresentationsOperationClaims;

namespace Application.Features.TeamMemberPresentations.Queries.GetList;

public class GetListTeamMemberPresentationQuery : IRequest<GetListResponse<GetListTeamMemberPresentationListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListTeamMemberPresentationQueryHandler : IRequestHandler<GetListTeamMemberPresentationQuery, GetListResponse<GetListTeamMemberPresentationListItemDto>>
    {
        private readonly ITeamMemberPresentationRepository _teamMemberPresentationRepository;
        private readonly IMapper _mapper;

        public GetListTeamMemberPresentationQueryHandler(ITeamMemberPresentationRepository teamMemberPresentationRepository, IMapper mapper)
        {
            _teamMemberPresentationRepository = teamMemberPresentationRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTeamMemberPresentationListItemDto>> Handle(GetListTeamMemberPresentationQuery request, CancellationToken cancellationToken)
        {
            IPaginate<TeamMemberPresentation> teamMemberPresentations = await _teamMemberPresentationRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTeamMemberPresentationListItemDto> response = _mapper.Map<GetListResponse<GetListTeamMemberPresentationListItemDto>>(teamMemberPresentations);
            return response;
        }
    }
}
using Application.Features.TeamMembers.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.TeamMembers.Constants.TeamMembersOperationClaims;

namespace Application.Features.TeamMembers.Queries.GetList;

public class GetListTeamMemberQuery : IRequest<GetListResponse<GetListTeamMemberListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    // public string[] Roles => new[] { Admin, Read };

    public class GetListTeamMemberQueryHandler : IRequestHandler<GetListTeamMemberQuery, GetListResponse<GetListTeamMemberListItemDto>>
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IMapper _mapper;

        public GetListTeamMemberQueryHandler(ITeamMemberRepository teamMemberRepository, IMapper mapper)
        {
            _teamMemberRepository = teamMemberRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTeamMemberListItemDto>> Handle(GetListTeamMemberQuery request, CancellationToken cancellationToken)
        {
            IPaginate<TeamMember> teamMembers = await _teamMemberRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTeamMemberListItemDto> response = _mapper.Map<GetListResponse<GetListTeamMemberListItemDto>>(teamMembers);
            return response;
        }
    }
}
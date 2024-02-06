using Application.Features.TeamMembers.Constants;
using Application.Features.TeamMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeamMembers.Constants.TeamMembersOperationClaims;

namespace Application.Features.TeamMembers.Queries.GetById;

public class GetByIdTeamMemberQuery : IRequest<GetByIdTeamMemberResponse>
{
    public Guid Id { get; set; }

    // public string[] Roles => new[] { Admin, Read };

    public class GetByIdTeamMemberQueryHandler : IRequestHandler<GetByIdTeamMemberQuery, GetByIdTeamMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly TeamMemberBusinessRules _teamMemberBusinessRules;

        public GetByIdTeamMemberQueryHandler(IMapper mapper, ITeamMemberRepository teamMemberRepository, TeamMemberBusinessRules teamMemberBusinessRules)
        {
            _mapper = mapper;
            _teamMemberRepository = teamMemberRepository;
            _teamMemberBusinessRules = teamMemberBusinessRules;
        }

        public async Task<GetByIdTeamMemberResponse> Handle(GetByIdTeamMemberQuery request, CancellationToken cancellationToken)
        {
            TeamMember? teamMember = await _teamMemberRepository.GetAsync(predicate: tm => tm.Id == request.Id, cancellationToken: cancellationToken);
            await _teamMemberBusinessRules.TeamMemberShouldExistWhenSelected(teamMember);

            GetByIdTeamMemberResponse response = _mapper.Map<GetByIdTeamMemberResponse>(teamMember);
            return response;
        }
    }
}
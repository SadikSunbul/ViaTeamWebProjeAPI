using Application.Features.TeamMembers.Constants;
using Application.Features.TeamMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeamMembers.Constants.TeamMembersOperationClaims;

namespace Application.Features.TeamMembers.Commands.Create;

public class CreateTeamMemberCommand : IRequest<CreatedTeamMemberResponse>, ISecuredRequest
{
    public Guid TeamId { get; set; }
    public string PhoneNumber { get; set; }
    public string Presentation { get; set; }

    public string[] Roles => new[] { Admin, Write, TeamMembersOperationClaims.Create };

    public class CreateTeamMemberCommandHandler : IRequestHandler<CreateTeamMemberCommand, CreatedTeamMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly TeamMemberBusinessRules _teamMemberBusinessRules;

        public CreateTeamMemberCommandHandler(IMapper mapper, ITeamMemberRepository teamMemberRepository,
                                         TeamMemberBusinessRules teamMemberBusinessRules)
        {
            _mapper = mapper;
            _teamMemberRepository = teamMemberRepository;
            _teamMemberBusinessRules = teamMemberBusinessRules;
        }

        public async Task<CreatedTeamMemberResponse> Handle(CreateTeamMemberCommand request, CancellationToken cancellationToken)
        {
            TeamMember teamMember = _mapper.Map<TeamMember>(request);

            await _teamMemberRepository.AddAsync(teamMember);

            CreatedTeamMemberResponse response = _mapper.Map<CreatedTeamMemberResponse>(teamMember);
            return response;
        }
    }
}
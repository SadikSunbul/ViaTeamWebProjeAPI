using Application.Features.TeamMembers.Constants;
using Application.Features.TeamMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeamMembers.Constants.TeamMembersOperationClaims;

namespace Application.Features.TeamMembers.Commands.Update;

public class UpdateTeamMemberCommand : IRequest<UpdatedTeamMemberResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public string PhoneNumber { get; set; }
    public string Presentation { get; set; }

    public string[] Roles => new[] { Admin, Write, TeamMembersOperationClaims.Update };

    public class UpdateTeamMemberCommandHandler : IRequestHandler<UpdateTeamMemberCommand, UpdatedTeamMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly TeamMemberBusinessRules _teamMemberBusinessRules;

        public UpdateTeamMemberCommandHandler(IMapper mapper, ITeamMemberRepository teamMemberRepository,
                                         TeamMemberBusinessRules teamMemberBusinessRules)
        {
            _mapper = mapper;
            _teamMemberRepository = teamMemberRepository;
            _teamMemberBusinessRules = teamMemberBusinessRules;
        }

        public async Task<UpdatedTeamMemberResponse> Handle(UpdateTeamMemberCommand request, CancellationToken cancellationToken)
        {
            TeamMember? teamMember = await _teamMemberRepository.GetAsync(predicate: tm => tm.Id == request.Id, cancellationToken: cancellationToken);
            await _teamMemberBusinessRules.TeamMemberShouldExistWhenSelected(teamMember);
            teamMember = _mapper.Map(request, teamMember);

            await _teamMemberRepository.UpdateAsync(teamMember!);

            UpdatedTeamMemberResponse response = _mapper.Map<UpdatedTeamMemberResponse>(teamMember);
            return response;
        }
    }
}
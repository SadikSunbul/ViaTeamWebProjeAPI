using Application.Features.TeamMembers.Constants;
using Application.Features.TeamMembers.Constants;
using Application.Features.TeamMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeamMembers.Constants.TeamMembersOperationClaims;

namespace Application.Features.TeamMembers.Commands.Delete;

public class DeleteTeamMemberCommand : IRequest<DeletedTeamMemberResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, TeamMembersOperationClaims.Delete };

    public class DeleteTeamMemberCommandHandler : IRequestHandler<DeleteTeamMemberCommand, DeletedTeamMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly TeamMemberBusinessRules _teamMemberBusinessRules;

        public DeleteTeamMemberCommandHandler(IMapper mapper, ITeamMemberRepository teamMemberRepository,
                                         TeamMemberBusinessRules teamMemberBusinessRules)
        {
            _mapper = mapper;
            _teamMemberRepository = teamMemberRepository;
            _teamMemberBusinessRules = teamMemberBusinessRules;
        }

        public async Task<DeletedTeamMemberResponse> Handle(DeleteTeamMemberCommand request, CancellationToken cancellationToken)
        {
            TeamMember? teamMember = await _teamMemberRepository.GetAsync(predicate: tm => tm.Id == request.Id, cancellationToken: cancellationToken);
            await _teamMemberBusinessRules.TeamMemberShouldExistWhenSelected(teamMember);

            await _teamMemberRepository.DeleteAsync(teamMember!,true);

            DeletedTeamMemberResponse response = _mapper.Map<DeletedTeamMemberResponse>(teamMember);
            return response;
        }
    }
}
using Application.Features.TeamMembers.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.TeamMembers.Rules;

public class TeamMemberBusinessRules : BaseBusinessRules
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public TeamMemberBusinessRules(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    public Task TeamMemberShouldExistWhenSelected(TeamMember? teamMember)
    {
        if (teamMember == null)
            throw new BusinessException(TeamMembersBusinessMessages.TeamMemberNotExists);
        return Task.CompletedTask;
    }

    public async Task TeamMemberIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        TeamMember? teamMember = await _teamMemberRepository.GetAsync(
            predicate: tm => tm.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TeamMemberShouldExistWhenSelected(teamMember);
    }
}
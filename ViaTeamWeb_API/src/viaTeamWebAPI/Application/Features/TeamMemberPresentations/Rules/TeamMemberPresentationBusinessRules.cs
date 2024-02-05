using Application.Features.TeamMemberPresentations.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.TeamMemberPresentations.Rules;

public class TeamMemberPresentationBusinessRules : BaseBusinessRules
{
    private readonly ITeamMemberPresentationRepository _teamMemberPresentationRepository;

    public TeamMemberPresentationBusinessRules(ITeamMemberPresentationRepository teamMemberPresentationRepository)
    {
        _teamMemberPresentationRepository = teamMemberPresentationRepository;
    }

    public Task TeamMemberPresentationShouldExistWhenSelected(TeamMemberPresentation? teamMemberPresentation)
    {
        if (teamMemberPresentation == null)
            throw new BusinessException(TeamMemberPresentationsBusinessMessages.TeamMemberPresentationNotExists);
        return Task.CompletedTask;
    }

    public async Task TeamMemberPresentationIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        TeamMemberPresentation? teamMemberPresentation = await _teamMemberPresentationRepository.GetAsync(
            predicate: tmp => tmp.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TeamMemberPresentationShouldExistWhenSelected(teamMemberPresentation);
    }
}
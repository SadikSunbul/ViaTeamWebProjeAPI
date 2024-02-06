using Application.Features.TeamAbouts.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.TeamAbouts.Rules;

public class TeamAboutBusinessRules : BaseBusinessRules
{
    private readonly ITeamAboutRepository _teamAboutRepository;

    public TeamAboutBusinessRules(ITeamAboutRepository teamAboutRepository)
    {
        _teamAboutRepository = teamAboutRepository;
    }

    public Task TeamAboutShouldExistWhenSelected(TeamAbout? teamAbout)
    {
        if (teamAbout == null)
            throw new BusinessException(TeamAboutsBusinessMessages.TeamAboutNotExists);
        return Task.CompletedTask;
    }

    public async Task TeamAboutIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        TeamAbout? teamAbout = await _teamAboutRepository.GetAsync(
            predicate: ta => ta.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TeamAboutShouldExistWhenSelected(teamAbout);
    }
}
using Application.Features.TeamAbouts.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TeamAbouts;

public class TeamAboutsManager : ITeamAboutsService
{
    private readonly ITeamAboutRepository _teamAboutRepository;
    private readonly TeamAboutBusinessRules _teamAboutBusinessRules;

    public TeamAboutsManager(ITeamAboutRepository teamAboutRepository, TeamAboutBusinessRules teamAboutBusinessRules)
    {
        _teamAboutRepository = teamAboutRepository;
        _teamAboutBusinessRules = teamAboutBusinessRules;
    }

    public async Task<TeamAbout?> GetAsync(
        Expression<Func<TeamAbout, bool>> predicate,
        Func<IQueryable<TeamAbout>, IIncludableQueryable<TeamAbout, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        TeamAbout? teamAbout = await _teamAboutRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return teamAbout;
    }

    public async Task<IPaginate<TeamAbout>?> GetListAsync(
        Expression<Func<TeamAbout, bool>>? predicate = null,
        Func<IQueryable<TeamAbout>, IOrderedQueryable<TeamAbout>>? orderBy = null,
        Func<IQueryable<TeamAbout>, IIncludableQueryable<TeamAbout, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<TeamAbout> teamAboutList = await _teamAboutRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return teamAboutList;
    }

    public async Task<TeamAbout> AddAsync(TeamAbout teamAbout)
    {
        TeamAbout addedTeamAbout = await _teamAboutRepository.AddAsync(teamAbout);

        return addedTeamAbout;
    }

    public async Task<TeamAbout> UpdateAsync(TeamAbout teamAbout)
    {
        TeamAbout updatedTeamAbout = await _teamAboutRepository.UpdateAsync(teamAbout);

        return updatedTeamAbout;
    }

    public async Task<TeamAbout> DeleteAsync(TeamAbout teamAbout, bool permanent = false)
    {
        TeamAbout deletedTeamAbout = await _teamAboutRepository.DeleteAsync(teamAbout);

        return deletedTeamAbout;
    }
}

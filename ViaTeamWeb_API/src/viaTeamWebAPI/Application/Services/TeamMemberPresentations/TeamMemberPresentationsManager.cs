using Application.Features.TeamMemberPresentations.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TeamMemberPresentations;

public class TeamMemberPresentationsManager : ITeamMemberPresentationsService
{
    private readonly ITeamMemberPresentationRepository _teamMemberPresentationRepository;
    private readonly TeamMemberPresentationBusinessRules _teamMemberPresentationBusinessRules;

    public TeamMemberPresentationsManager(ITeamMemberPresentationRepository teamMemberPresentationRepository, TeamMemberPresentationBusinessRules teamMemberPresentationBusinessRules)
    {
        _teamMemberPresentationRepository = teamMemberPresentationRepository;
        _teamMemberPresentationBusinessRules = teamMemberPresentationBusinessRules;
    }

    public async Task<TeamMemberPresentation?> GetAsync(
        Expression<Func<TeamMemberPresentation, bool>> predicate,
        Func<IQueryable<TeamMemberPresentation>, IIncludableQueryable<TeamMemberPresentation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        TeamMemberPresentation? teamMemberPresentation = await _teamMemberPresentationRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return teamMemberPresentation;
    }

    public async Task<IPaginate<TeamMemberPresentation>?> GetListAsync(
        Expression<Func<TeamMemberPresentation, bool>>? predicate = null,
        Func<IQueryable<TeamMemberPresentation>, IOrderedQueryable<TeamMemberPresentation>>? orderBy = null,
        Func<IQueryable<TeamMemberPresentation>, IIncludableQueryable<TeamMemberPresentation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<TeamMemberPresentation> teamMemberPresentationList = await _teamMemberPresentationRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return teamMemberPresentationList;
    }

    public async Task<TeamMemberPresentation> AddAsync(TeamMemberPresentation teamMemberPresentation)
    {
        TeamMemberPresentation addedTeamMemberPresentation = await _teamMemberPresentationRepository.AddAsync(teamMemberPresentation);

        return addedTeamMemberPresentation;
    }

    public async Task<TeamMemberPresentation> UpdateAsync(TeamMemberPresentation teamMemberPresentation)
    {
        TeamMemberPresentation updatedTeamMemberPresentation = await _teamMemberPresentationRepository.UpdateAsync(teamMemberPresentation);

        return updatedTeamMemberPresentation;
    }

    public async Task<TeamMemberPresentation> DeleteAsync(TeamMemberPresentation teamMemberPresentation, bool permanent = false)
    {
        TeamMemberPresentation deletedTeamMemberPresentation = await _teamMemberPresentationRepository.DeleteAsync(teamMemberPresentation);

        return deletedTeamMemberPresentation;
    }
}

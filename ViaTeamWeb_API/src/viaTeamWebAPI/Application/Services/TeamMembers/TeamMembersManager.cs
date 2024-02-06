using Application.Features.TeamMembers.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TeamMembers;

public class TeamMembersManager : ITeamMembersService
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly TeamMemberBusinessRules _teamMemberBusinessRules;

    public TeamMembersManager(ITeamMemberRepository teamMemberRepository, TeamMemberBusinessRules teamMemberBusinessRules)
    {
        _teamMemberRepository = teamMemberRepository;
        _teamMemberBusinessRules = teamMemberBusinessRules;
    }

    public async Task<TeamMember?> GetAsync(
        Expression<Func<TeamMember, bool>> predicate,
        Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        TeamMember? teamMember = await _teamMemberRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return teamMember;
    }

    public async Task<IPaginate<TeamMember>?> GetListAsync(
        Expression<Func<TeamMember, bool>>? predicate = null,
        Func<IQueryable<TeamMember>, IOrderedQueryable<TeamMember>>? orderBy = null,
        Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<TeamMember> teamMemberList = await _teamMemberRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return teamMemberList;
    }

    public async Task<TeamMember> AddAsync(TeamMember teamMember)
    {
        TeamMember addedTeamMember = await _teamMemberRepository.AddAsync(teamMember);

        return addedTeamMember;
    }

    public async Task<TeamMember> UpdateAsync(TeamMember teamMember)
    {
        TeamMember updatedTeamMember = await _teamMemberRepository.UpdateAsync(teamMember);

        return updatedTeamMember;
    }

    public async Task<TeamMember> DeleteAsync(TeamMember teamMember, bool permanent = false)
    {
        TeamMember deletedTeamMember = await _teamMemberRepository.DeleteAsync(teamMember);

        return deletedTeamMember;
    }
}

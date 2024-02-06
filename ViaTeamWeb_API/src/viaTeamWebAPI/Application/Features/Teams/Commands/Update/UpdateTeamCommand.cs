using Application.Features.Teams.Constants;
using Application.Features.Teams.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Teams.Constants.TeamsOperationClaims;

namespace Application.Features.Teams.Commands.Update;

public class UpdateTeamCommand : IRequest<UpdatedTeamResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid TeamAboutId { get; set; }

    public string[] Roles => new[] { Admin, Write, TeamsOperationClaims.Update };

    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, UpdatedTeamResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly TeamBusinessRules _teamBusinessRules;

        public UpdateTeamCommandHandler(IMapper mapper, ITeamRepository teamRepository,
                                         TeamBusinessRules teamBusinessRules)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _teamBusinessRules = teamBusinessRules;
        }

        public async Task<UpdatedTeamResponse> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            Team? team = await _teamRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _teamBusinessRules.TeamShouldExistWhenSelected(team);
            team = _mapper.Map(request, team);

            await _teamRepository.UpdateAsync(team!);

            UpdatedTeamResponse response = _mapper.Map<UpdatedTeamResponse>(team);
            return response;
        }
    }
}
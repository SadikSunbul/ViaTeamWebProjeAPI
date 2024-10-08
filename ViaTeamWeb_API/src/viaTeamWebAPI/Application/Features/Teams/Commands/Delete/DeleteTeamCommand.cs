using Application.Features.Teams.Constants;
using Application.Features.Teams.Constants;
using Application.Features.Teams.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Teams.Constants.TeamsOperationClaims;

namespace Application.Features.Teams.Commands.Delete;

public class DeleteTeamCommand : IRequest<DeletedTeamResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, TeamsOperationClaims.Delete };

    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, DeletedTeamResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly TeamBusinessRules _teamBusinessRules;

        public DeleteTeamCommandHandler(IMapper mapper, ITeamRepository teamRepository,
                                         TeamBusinessRules teamBusinessRules)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _teamBusinessRules = teamBusinessRules;
        }

        public async Task<DeletedTeamResponse> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            Team? team = await _teamRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _teamBusinessRules.TeamShouldExistWhenSelected(team);

            await _teamRepository.DeleteAsync(team!,true);

            DeletedTeamResponse response = _mapper.Map<DeletedTeamResponse>(team);
            return response;
        }
    }
}
using Application.Features.Teams.Constants;
using Application.Features.Teams.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Teams.Constants.TeamsOperationClaims;

namespace Application.Features.Teams.Commands.Create;

public class CreateTeamCommand : IRequest<CreatedTeamResponse>, ISecuredRequest
{
    public string Name { get; set; }
    public Guid TeamAboutId { get; set; }
    
    public string[] Roles => new[] { Admin, Write, TeamsOperationClaims.Create };

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, CreatedTeamResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly TeamBusinessRules _teamBusinessRules;

        public CreateTeamCommandHandler(IMapper mapper, ITeamRepository teamRepository,
                                         TeamBusinessRules teamBusinessRules)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _teamBusinessRules = teamBusinessRules;
        }

        public async Task<CreatedTeamResponse> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            Team team = _mapper.Map<Team>(request);

            await _teamRepository.AddAsync(team);

            CreatedTeamResponse response = _mapper.Map<CreatedTeamResponse>(team);
            return response;
        }
    }
}
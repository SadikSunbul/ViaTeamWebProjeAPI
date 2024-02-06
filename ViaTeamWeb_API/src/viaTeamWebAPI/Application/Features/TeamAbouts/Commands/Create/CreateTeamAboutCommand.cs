using Application.Features.TeamAbouts.Constants;
using Application.Features.TeamAbouts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeamAbouts.Constants.TeamAboutsOperationClaims;

namespace Application.Features.TeamAbouts.Commands.Create;

public class CreateTeamAboutCommand : IRequest<CreatedTeamAboutResponse>, ISecuredRequest
{
    public string Title1 { get; set; }
    public string Prg1 { get; set; }
    public string Title2 { get; set; }
    public string Prg2 { get; set; }

    public string[] Roles => new[] { Admin, Write, TeamAboutsOperationClaims.Create };

    public class CreateTeamAboutCommandHandler : IRequestHandler<CreateTeamAboutCommand, CreatedTeamAboutResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamAboutRepository _teamAboutRepository;
        private readonly TeamAboutBusinessRules _teamAboutBusinessRules;

        public CreateTeamAboutCommandHandler(IMapper mapper, ITeamAboutRepository teamAboutRepository,
                                         TeamAboutBusinessRules teamAboutBusinessRules)
        {
            _mapper = mapper;
            _teamAboutRepository = teamAboutRepository;
            _teamAboutBusinessRules = teamAboutBusinessRules;
        }

        public async Task<CreatedTeamAboutResponse> Handle(CreateTeamAboutCommand request, CancellationToken cancellationToken)
        {
            TeamAbout teamAbout = _mapper.Map<TeamAbout>(request);

            await _teamAboutRepository.AddAsync(teamAbout);

            CreatedTeamAboutResponse response = _mapper.Map<CreatedTeamAboutResponse>(teamAbout);
            return response;
        }
    }
}
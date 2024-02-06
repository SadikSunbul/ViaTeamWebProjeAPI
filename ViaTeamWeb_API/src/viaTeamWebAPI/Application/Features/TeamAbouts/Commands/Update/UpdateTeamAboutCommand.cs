using Application.Features.TeamAbouts.Constants;
using Application.Features.TeamAbouts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeamAbouts.Constants.TeamAboutsOperationClaims;

namespace Application.Features.TeamAbouts.Commands.Update;

public class UpdateTeamAboutCommand : IRequest<UpdatedTeamAboutResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string Title1 { get; set; }
    public string Prg1 { get; set; }
    public string Title2 { get; set; }
    public string Prg2 { get; set; }

    public string[] Roles => new[] { Admin, Write, TeamAboutsOperationClaims.Update };

    public class UpdateTeamAboutCommandHandler : IRequestHandler<UpdateTeamAboutCommand, UpdatedTeamAboutResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamAboutRepository _teamAboutRepository;
        private readonly TeamAboutBusinessRules _teamAboutBusinessRules;

        public UpdateTeamAboutCommandHandler(IMapper mapper, ITeamAboutRepository teamAboutRepository,
                                         TeamAboutBusinessRules teamAboutBusinessRules)
        {
            _mapper = mapper;
            _teamAboutRepository = teamAboutRepository;
            _teamAboutBusinessRules = teamAboutBusinessRules;
        }

        public async Task<UpdatedTeamAboutResponse> Handle(UpdateTeamAboutCommand request, CancellationToken cancellationToken)
        {
            TeamAbout? teamAbout = await _teamAboutRepository.GetAsync(predicate: ta => ta.Id == request.Id, cancellationToken: cancellationToken);
            await _teamAboutBusinessRules.TeamAboutShouldExistWhenSelected(teamAbout);
            teamAbout = _mapper.Map(request, teamAbout);

            await _teamAboutRepository.UpdateAsync(teamAbout!);

            UpdatedTeamAboutResponse response = _mapper.Map<UpdatedTeamAboutResponse>(teamAbout);
            return response;
        }
    }
}
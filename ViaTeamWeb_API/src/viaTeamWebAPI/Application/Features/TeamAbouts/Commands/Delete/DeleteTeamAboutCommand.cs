using Application.Features.TeamAbouts.Constants;
using Application.Features.TeamAbouts.Constants;
using Application.Features.TeamAbouts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeamAbouts.Constants.TeamAboutsOperationClaims;

namespace Application.Features.TeamAbouts.Commands.Delete;

public class DeleteTeamAboutCommand : IRequest<DeletedTeamAboutResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, TeamAboutsOperationClaims.Delete };

    public class DeleteTeamAboutCommandHandler : IRequestHandler<DeleteTeamAboutCommand, DeletedTeamAboutResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamAboutRepository _teamAboutRepository;
        private readonly TeamAboutBusinessRules _teamAboutBusinessRules;

        public DeleteTeamAboutCommandHandler(IMapper mapper, ITeamAboutRepository teamAboutRepository,
                                         TeamAboutBusinessRules teamAboutBusinessRules)
        {
            _mapper = mapper;
            _teamAboutRepository = teamAboutRepository;
            _teamAboutBusinessRules = teamAboutBusinessRules;
        }

        public async Task<DeletedTeamAboutResponse> Handle(DeleteTeamAboutCommand request, CancellationToken cancellationToken)
        {
            TeamAbout? teamAbout = await _teamAboutRepository.GetAsync(predicate: ta => ta.Id == request.Id, cancellationToken: cancellationToken);
            await _teamAboutBusinessRules.TeamAboutShouldExistWhenSelected(teamAbout);

            await _teamAboutRepository.DeleteAsync(teamAbout!,true);

            DeletedTeamAboutResponse response = _mapper.Map<DeletedTeamAboutResponse>(teamAbout);
            return response;
        }
    }
}
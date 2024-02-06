using Application.Features.TeamAbouts.Constants;
using Application.Features.TeamAbouts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeamAbouts.Constants.TeamAboutsOperationClaims;

namespace Application.Features.TeamAbouts.Queries.GetById;

public class GetByIdTeamAboutQuery : IRequest<GetByIdTeamAboutResponse>
{
    public Guid Id { get; set; }

    // public string[] Roles => new[] { Admin, Read };

    public class GetByIdTeamAboutQueryHandler : IRequestHandler<GetByIdTeamAboutQuery, GetByIdTeamAboutResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamAboutRepository _teamAboutRepository;
        private readonly TeamAboutBusinessRules _teamAboutBusinessRules;

        public GetByIdTeamAboutQueryHandler(IMapper mapper, ITeamAboutRepository teamAboutRepository, TeamAboutBusinessRules teamAboutBusinessRules)
        {
            _mapper = mapper;
            _teamAboutRepository = teamAboutRepository;
            _teamAboutBusinessRules = teamAboutBusinessRules;
        }

        public async Task<GetByIdTeamAboutResponse> Handle(GetByIdTeamAboutQuery request, CancellationToken cancellationToken)
        {
            TeamAbout? teamAbout = await _teamAboutRepository.GetAsync(predicate: ta => ta.Id == request.Id, cancellationToken: cancellationToken);
            await _teamAboutBusinessRules.TeamAboutShouldExistWhenSelected(teamAbout);

            GetByIdTeamAboutResponse response = _mapper.Map<GetByIdTeamAboutResponse>(teamAbout);
            return response;
        }
    }
}
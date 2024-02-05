using Application.Features.TeamMemberPresentations.Constants;
using Application.Features.TeamMemberPresentations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeamMemberPresentations.Constants.TeamMemberPresentationsOperationClaims;

namespace Application.Features.TeamMemberPresentations.Commands.Create;

public class CreateTeamMemberPresentationCommand : IRequest<CreatedTeamMemberPresentationResponse>, ISecuredRequest
{
    public string Title { get; set; }
    public string Prg1 { get; set; }
    public string Prg2 { get; set; }

    public string[] Roles => new[] { Admin, Write, TeamMemberPresentationsOperationClaims.Create };

    public class CreateTeamMemberPresentationCommandHandler : IRequestHandler<CreateTeamMemberPresentationCommand, CreatedTeamMemberPresentationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamMemberPresentationRepository _teamMemberPresentationRepository;
        private readonly TeamMemberPresentationBusinessRules _teamMemberPresentationBusinessRules;

        public CreateTeamMemberPresentationCommandHandler(IMapper mapper, ITeamMemberPresentationRepository teamMemberPresentationRepository,
                                         TeamMemberPresentationBusinessRules teamMemberPresentationBusinessRules)
        {
            _mapper = mapper;
            _teamMemberPresentationRepository = teamMemberPresentationRepository;
            _teamMemberPresentationBusinessRules = teamMemberPresentationBusinessRules;
        }

        public async Task<CreatedTeamMemberPresentationResponse> Handle(CreateTeamMemberPresentationCommand request, CancellationToken cancellationToken)
        {
            TeamMemberPresentation teamMemberPresentation = _mapper.Map<TeamMemberPresentation>(request);

            await _teamMemberPresentationRepository.AddAsync(teamMemberPresentation);

            CreatedTeamMemberPresentationResponse response = _mapper.Map<CreatedTeamMemberPresentationResponse>(teamMemberPresentation);
            return response;
        }
    }
}
using Application.Features.TeamMemberPresentations.Constants;
using Application.Features.TeamMemberPresentations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeamMemberPresentations.Constants.TeamMemberPresentationsOperationClaims;

namespace Application.Features.TeamMemberPresentations.Commands.Update;

public class UpdateTeamMemberPresentationCommand : IRequest<UpdatedTeamMemberPresentationResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Prg1 { get; set; }
    public string Prg2 { get; set; }

    public string[] Roles => new[] { Admin, Write, TeamMemberPresentationsOperationClaims.Update };

    public class UpdateTeamMemberPresentationCommandHandler : IRequestHandler<UpdateTeamMemberPresentationCommand, UpdatedTeamMemberPresentationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamMemberPresentationRepository _teamMemberPresentationRepository;
        private readonly TeamMemberPresentationBusinessRules _teamMemberPresentationBusinessRules;

        public UpdateTeamMemberPresentationCommandHandler(IMapper mapper, ITeamMemberPresentationRepository teamMemberPresentationRepository,
                                         TeamMemberPresentationBusinessRules teamMemberPresentationBusinessRules)
        {
            _mapper = mapper;
            _teamMemberPresentationRepository = teamMemberPresentationRepository;
            _teamMemberPresentationBusinessRules = teamMemberPresentationBusinessRules;
        }

        public async Task<UpdatedTeamMemberPresentationResponse> Handle(UpdateTeamMemberPresentationCommand request, CancellationToken cancellationToken)
        {
            TeamMemberPresentation? teamMemberPresentation = await _teamMemberPresentationRepository.GetAsync(predicate: tmp => tmp.Id == request.Id, cancellationToken: cancellationToken);
            await _teamMemberPresentationBusinessRules.TeamMemberPresentationShouldExistWhenSelected(teamMemberPresentation);
            teamMemberPresentation = _mapper.Map(request, teamMemberPresentation);

            await _teamMemberPresentationRepository.UpdateAsync(teamMemberPresentation!);

            UpdatedTeamMemberPresentationResponse response = _mapper.Map<UpdatedTeamMemberPresentationResponse>(teamMemberPresentation);
            return response;
        }
    }
}
using Application.Features.TeamMemberPresentations.Constants;
using Application.Features.TeamMemberPresentations.Constants;
using Application.Features.TeamMemberPresentations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeamMemberPresentations.Constants.TeamMemberPresentationsOperationClaims;

namespace Application.Features.TeamMemberPresentations.Commands.Delete;

public class DeleteTeamMemberPresentationCommand : IRequest<DeletedTeamMemberPresentationResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, TeamMemberPresentationsOperationClaims.Delete };

    public class DeleteTeamMemberPresentationCommandHandler : IRequestHandler<DeleteTeamMemberPresentationCommand, DeletedTeamMemberPresentationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamMemberPresentationRepository _teamMemberPresentationRepository;
        private readonly TeamMemberPresentationBusinessRules _teamMemberPresentationBusinessRules;

        public DeleteTeamMemberPresentationCommandHandler(IMapper mapper, ITeamMemberPresentationRepository teamMemberPresentationRepository,
                                         TeamMemberPresentationBusinessRules teamMemberPresentationBusinessRules)
        {
            _mapper = mapper;
            _teamMemberPresentationRepository = teamMemberPresentationRepository;
            _teamMemberPresentationBusinessRules = teamMemberPresentationBusinessRules;
        }

        public async Task<DeletedTeamMemberPresentationResponse> Handle(DeleteTeamMemberPresentationCommand request, CancellationToken cancellationToken)
        {
            TeamMemberPresentation? teamMemberPresentation = await _teamMemberPresentationRepository.GetAsync(predicate: tmp => tmp.Id == request.Id, cancellationToken: cancellationToken);
            await _teamMemberPresentationBusinessRules.TeamMemberPresentationShouldExistWhenSelected(teamMemberPresentation);

            await _teamMemberPresentationRepository.DeleteAsync(teamMemberPresentation!,true);

            DeletedTeamMemberPresentationResponse response = _mapper.Map<DeletedTeamMemberPresentationResponse>(teamMemberPresentation);
            return response;
        }
    }
}
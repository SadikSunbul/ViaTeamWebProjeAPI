using Application.Features.TeamMemberPresentations.Constants;
using Application.Features.TeamMemberPresentations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeamMemberPresentations.Constants.TeamMemberPresentationsOperationClaims;

namespace Application.Features.TeamMemberPresentations.Queries.GetById;

public class GetByIdTeamMemberPresentationQuery : IRequest<GetByIdTeamMemberPresentationResponse>
{
    public Guid Id { get; set; }

    // public string[] Roles => new[] { Admin, Read };

    public class GetByIdTeamMemberPresentationQueryHandler : IRequestHandler<GetByIdTeamMemberPresentationQuery, GetByIdTeamMemberPresentationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamMemberPresentationRepository _teamMemberPresentationRepository;
        private readonly TeamMemberPresentationBusinessRules _teamMemberPresentationBusinessRules;

        public GetByIdTeamMemberPresentationQueryHandler(IMapper mapper, ITeamMemberPresentationRepository teamMemberPresentationRepository, TeamMemberPresentationBusinessRules teamMemberPresentationBusinessRules)
        {
            _mapper = mapper;
            _teamMemberPresentationRepository = teamMemberPresentationRepository;
            _teamMemberPresentationBusinessRules = teamMemberPresentationBusinessRules;
        }

        public async Task<GetByIdTeamMemberPresentationResponse> Handle(GetByIdTeamMemberPresentationQuery request, CancellationToken cancellationToken)
        {
            TeamMemberPresentation? teamMemberPresentation = await _teamMemberPresentationRepository.GetAsync(predicate: tmp => tmp.Id == request.Id, cancellationToken: cancellationToken);
            await _teamMemberPresentationBusinessRules.TeamMemberPresentationShouldExistWhenSelected(teamMemberPresentation);

            GetByIdTeamMemberPresentationResponse response = _mapper.Map<GetByIdTeamMemberPresentationResponse>(teamMemberPresentation);
            return response;
        }
    }
}
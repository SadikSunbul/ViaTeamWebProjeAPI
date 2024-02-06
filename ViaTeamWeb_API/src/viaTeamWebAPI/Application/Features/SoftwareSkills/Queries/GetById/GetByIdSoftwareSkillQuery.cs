using Application.Features.SoftwareSkills.Constants;
using Application.Features.SoftwareSkills.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.SoftwareSkills.Constants.SoftwareSkillsOperationClaims;

namespace Application.Features.SoftwareSkills.Queries.GetById;

public class GetByIdSoftwareSkillQuery : IRequest<GetByIdSoftwareSkillResponse>
{
    public Guid Id { get; set; }

    // public string[] Roles => new[] { Admin, Read };

    public class GetByIdSoftwareSkillQueryHandler : IRequestHandler<GetByIdSoftwareSkillQuery, GetByIdSoftwareSkillResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISoftwareSkillRepository _softwareSkillRepository;
        private readonly SoftwareSkillBusinessRules _softwareSkillBusinessRules;

        public GetByIdSoftwareSkillQueryHandler(IMapper mapper, ISoftwareSkillRepository softwareSkillRepository, SoftwareSkillBusinessRules softwareSkillBusinessRules)
        {
            _mapper = mapper;
            _softwareSkillRepository = softwareSkillRepository;
            _softwareSkillBusinessRules = softwareSkillBusinessRules;
        }

        public async Task<GetByIdSoftwareSkillResponse> Handle(GetByIdSoftwareSkillQuery request, CancellationToken cancellationToken)
        {
            SoftwareSkill? softwareSkill = await _softwareSkillRepository.GetAsync(predicate: ss => ss.Id == request.Id, cancellationToken: cancellationToken);
            await _softwareSkillBusinessRules.SoftwareSkillShouldExistWhenSelected(softwareSkill);

            GetByIdSoftwareSkillResponse response = _mapper.Map<GetByIdSoftwareSkillResponse>(softwareSkill);
            return response;
        }
    }
}
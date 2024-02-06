using Application.Features.SoftwareSkills.Constants;
using Application.Features.SoftwareSkills.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.SoftwareSkills.Constants.SoftwareSkillsOperationClaims;

namespace Application.Features.SoftwareSkills.Commands.Create;

public class CreateSoftwareSkillCommand : IRequest<CreatedSoftwareSkillResponse>, ISecuredRequest
{
    public string SkillName { get; set; }
    public string SkillPercent { get; set; }

    public string[] Roles => new[] { Admin, Write, SoftwareSkillsOperationClaims.Create };

    public class CreateSoftwareSkillCommandHandler : IRequestHandler<CreateSoftwareSkillCommand, CreatedSoftwareSkillResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISoftwareSkillRepository _softwareSkillRepository;
        private readonly SoftwareSkillBusinessRules _softwareSkillBusinessRules;

        public CreateSoftwareSkillCommandHandler(IMapper mapper, ISoftwareSkillRepository softwareSkillRepository,
                                         SoftwareSkillBusinessRules softwareSkillBusinessRules)
        {
            _mapper = mapper;
            _softwareSkillRepository = softwareSkillRepository;
            _softwareSkillBusinessRules = softwareSkillBusinessRules;
        }

        public async Task<CreatedSoftwareSkillResponse> Handle(CreateSoftwareSkillCommand request, CancellationToken cancellationToken)
        {
            SoftwareSkill softwareSkill = _mapper.Map<SoftwareSkill>(request);

            await _softwareSkillRepository.AddAsync(softwareSkill);

            CreatedSoftwareSkillResponse response = _mapper.Map<CreatedSoftwareSkillResponse>(softwareSkill);
            return response;
        }
    }
}
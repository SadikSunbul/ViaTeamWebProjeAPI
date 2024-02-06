using Application.Features.SoftwareSkills.Constants;
using Application.Features.SoftwareSkills.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.SoftwareSkills.Constants.SoftwareSkillsOperationClaims;

namespace Application.Features.SoftwareSkills.Commands.Update;

public class UpdateSoftwareSkillCommand : IRequest<UpdatedSoftwareSkillResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string SkillName { get; set; }
    public string SkillPercent { get; set; }

    public string[] Roles => new[] { Admin, Write, SoftwareSkillsOperationClaims.Update };

    public class UpdateSoftwareSkillCommandHandler : IRequestHandler<UpdateSoftwareSkillCommand, UpdatedSoftwareSkillResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISoftwareSkillRepository _softwareSkillRepository;
        private readonly SoftwareSkillBusinessRules _softwareSkillBusinessRules;

        public UpdateSoftwareSkillCommandHandler(IMapper mapper, ISoftwareSkillRepository softwareSkillRepository,
                                         SoftwareSkillBusinessRules softwareSkillBusinessRules)
        {
            _mapper = mapper;
            _softwareSkillRepository = softwareSkillRepository;
            _softwareSkillBusinessRules = softwareSkillBusinessRules;
        }

        public async Task<UpdatedSoftwareSkillResponse> Handle(UpdateSoftwareSkillCommand request, CancellationToken cancellationToken)
        {
            SoftwareSkill? softwareSkill = await _softwareSkillRepository.GetAsync(predicate: ss => ss.Id == request.Id, cancellationToken: cancellationToken);
            await _softwareSkillBusinessRules.SoftwareSkillShouldExistWhenSelected(softwareSkill);
            softwareSkill = _mapper.Map(request, softwareSkill);

            await _softwareSkillRepository.UpdateAsync(softwareSkill!);

            UpdatedSoftwareSkillResponse response = _mapper.Map<UpdatedSoftwareSkillResponse>(softwareSkill);
            return response;
        }
    }
}
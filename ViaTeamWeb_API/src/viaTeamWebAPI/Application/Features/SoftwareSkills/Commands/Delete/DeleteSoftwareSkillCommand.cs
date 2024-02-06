using Application.Features.SoftwareSkills.Constants;
using Application.Features.SoftwareSkills.Constants;
using Application.Features.SoftwareSkills.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.SoftwareSkills.Constants.SoftwareSkillsOperationClaims;

namespace Application.Features.SoftwareSkills.Commands.Delete;

public class DeleteSoftwareSkillCommand : IRequest<DeletedSoftwareSkillResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, SoftwareSkillsOperationClaims.Delete };

    public class DeleteSoftwareSkillCommandHandler : IRequestHandler<DeleteSoftwareSkillCommand, DeletedSoftwareSkillResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISoftwareSkillRepository _softwareSkillRepository;
        private readonly SoftwareSkillBusinessRules _softwareSkillBusinessRules;

        public DeleteSoftwareSkillCommandHandler(IMapper mapper, ISoftwareSkillRepository softwareSkillRepository,
                                         SoftwareSkillBusinessRules softwareSkillBusinessRules)
        {
            _mapper = mapper;
            _softwareSkillRepository = softwareSkillRepository;
            _softwareSkillBusinessRules = softwareSkillBusinessRules;
        }

        public async Task<DeletedSoftwareSkillResponse> Handle(DeleteSoftwareSkillCommand request, CancellationToken cancellationToken)
        {
            SoftwareSkill? softwareSkill = await _softwareSkillRepository.GetAsync(predicate: ss => ss.Id == request.Id, cancellationToken: cancellationToken);
            await _softwareSkillBusinessRules.SoftwareSkillShouldExistWhenSelected(softwareSkill);

            await _softwareSkillRepository.DeleteAsync(softwareSkill!,true);

            DeletedSoftwareSkillResponse response = _mapper.Map<DeletedSoftwareSkillResponse>(softwareSkill);
            return response;
        }
    }
}
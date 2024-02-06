using Application.Features.SoftwareSkillMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.SoftwareSkillMembers.Commands.Update;

public class UpdateSoftwareSkillMemberCommand : IRequest<UpdatedSoftwareSkillMemberResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid SoftwareSkillId { get; set; }
    public Guid MemberId { get; set; }
    public SoftwareSkill SoftwareSkill { get; set; }
    public Member Member { get; set; }

    public class UpdateSoftwareSkillMemberCommandHandler : IRequestHandler<UpdateSoftwareSkillMemberCommand, UpdatedSoftwareSkillMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISoftwareSkillMemberRepository _softwareSkillMemberRepository;
        private readonly SoftwareSkillMemberBusinessRules _softwareSkillMemberBusinessRules;

        public UpdateSoftwareSkillMemberCommandHandler(IMapper mapper, ISoftwareSkillMemberRepository softwareSkillMemberRepository,
                                         SoftwareSkillMemberBusinessRules softwareSkillMemberBusinessRules)
        {
            _mapper = mapper;
            _softwareSkillMemberRepository = softwareSkillMemberRepository;
            _softwareSkillMemberBusinessRules = softwareSkillMemberBusinessRules;
        }

        public async Task<UpdatedSoftwareSkillMemberResponse> Handle(UpdateSoftwareSkillMemberCommand request, CancellationToken cancellationToken)
        {
            SoftwareSkillMember? softwareSkillMember = await _softwareSkillMemberRepository.GetAsync(predicate: ssm => ssm.Id == request.Id, cancellationToken: cancellationToken);
            await _softwareSkillMemberBusinessRules.SoftwareSkillMemberShouldExistWhenSelected(softwareSkillMember);
            softwareSkillMember = _mapper.Map(request, softwareSkillMember);

            await _softwareSkillMemberRepository.UpdateAsync(softwareSkillMember!);

            UpdatedSoftwareSkillMemberResponse response = _mapper.Map<UpdatedSoftwareSkillMemberResponse>(softwareSkillMember);
            return response;
        }
    }
}
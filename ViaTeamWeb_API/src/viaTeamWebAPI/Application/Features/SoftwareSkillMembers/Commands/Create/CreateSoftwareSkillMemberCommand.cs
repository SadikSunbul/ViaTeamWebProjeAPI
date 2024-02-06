using Application.Features.SoftwareSkillMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.SoftwareSkillMembers.Commands.Create;

public class CreateSoftwareSkillMemberCommand : IRequest<CreatedSoftwareSkillMemberResponse>, ITransactionalRequest
{
    public Guid SoftwareSkillId { get; set; }
    public int UserId { get; set; }

    public class CreateSoftwareSkillMemberCommandHandler : IRequestHandler<CreateSoftwareSkillMemberCommand, CreatedSoftwareSkillMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly ISoftwareSkillMemberRepository _softwareSkillMemberRepository;
        private readonly SoftwareSkillMemberBusinessRules _softwareSkillMemberBusinessRules;

        public CreateSoftwareSkillMemberCommandHandler(IMapper mapper, ISoftwareSkillMemberRepository softwareSkillMemberRepository,
                                         SoftwareSkillMemberBusinessRules softwareSkillMemberBusinessRules, IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _softwareSkillMemberRepository = softwareSkillMemberRepository;
            _softwareSkillMemberBusinessRules = softwareSkillMemberBusinessRules;
            _memberRepository = memberRepository;
        }

        public async Task<CreatedSoftwareSkillMemberResponse> Handle(CreateSoftwareSkillMemberCommand request, CancellationToken cancellationToken)
        {
            Guid MemberId = (await _memberRepository.GetAsync(i => i.UserId == request.UserId)).Id;
            SoftwareSkillMember softwareSkillMember = new();
            softwareSkillMember.MemberId = MemberId;
            softwareSkillMember.SoftwareSkillId = request.SoftwareSkillId;

            await _softwareSkillMemberRepository.AddAsync(softwareSkillMember);

            CreatedSoftwareSkillMemberResponse response = _mapper.Map<CreatedSoftwareSkillMemberResponse>(softwareSkillMember);
            return response;
        }
    }
}
using Application.Features.SoftwareSkillMembers.Constants;
using Application.Features.SoftwareSkillMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.SoftwareSkillMembers.Commands.Delete;

public class DeleteSoftwareSkillMemberCommand : IRequest<DeletedSoftwareSkillMemberResponse>,
    ITransactionalRequest
{
    public Guid Id { get; set; }
    public int UserId { get; set; }

    public class DeleteSoftwareSkillMemberCommandHandler : IRequestHandler<DeleteSoftwareSkillMemberCommand,
        DeletedSoftwareSkillMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly ISoftwareSkillMemberRepository _softwareSkillMemberRepository;
        private readonly SoftwareSkillMemberBusinessRules _softwareSkillMemberBusinessRules;

        public DeleteSoftwareSkillMemberCommandHandler(IMapper mapper,
            ISoftwareSkillMemberRepository softwareSkillMemberRepository,
            SoftwareSkillMemberBusinessRules softwareSkillMemberBusinessRules,
            IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _softwareSkillMemberRepository = softwareSkillMemberRepository;
            _softwareSkillMemberBusinessRules = softwareSkillMemberBusinessRules;
            _memberRepository = memberRepository;
        }

        public async Task<DeletedSoftwareSkillMemberResponse> Handle(DeleteSoftwareSkillMemberCommand request,
            CancellationToken cancellationToken)
        {
            Guid MemberId = (await _memberRepository.GetAsync(i => i.UserId == request.UserId)).Id;
            SoftwareSkillMember? softwareSkillMember =
                await _softwareSkillMemberRepository.GetAsync(
                    predicate: ssm => ssm.Id == request.Id && ssm.MemberId == MemberId,
                    cancellationToken: cancellationToken);
            await _softwareSkillMemberBusinessRules.SoftwareSkillMemberShouldExistWhenSelected(
                softwareSkillMember);

            await _softwareSkillMemberRepository.DeleteAsync(softwareSkillMember!,true);

            DeletedSoftwareSkillMemberResponse response =
                _mapper.Map<DeletedSoftwareSkillMemberResponse>(softwareSkillMember);
            return response;
        }
    }
}
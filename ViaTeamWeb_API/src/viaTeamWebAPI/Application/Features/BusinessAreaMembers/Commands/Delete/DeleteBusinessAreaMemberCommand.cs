using Application.Features.BusinessAreaMembers.Constants;
using Application.Features.BusinessAreaMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Transaction;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.BusinessAreaMembers.Commands.Delete;

public class DeleteBusinessAreaMemberCommand : IRequest<DeletedBusinessAreaMemberResponse>,
    ITransactionalRequest
{
    public Guid Id { get; set; }
    public int UserId { get; set; }

    public class DeleteBusinessAreaMemberCommandHandler : IRequestHandler<DeleteBusinessAreaMemberCommand,
        DeletedBusinessAreaMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly IBusinessAreaMemberRepository _businessAreaMemberRepository;
        private readonly BusinessAreaMemberBusinessRules _businessAreaMemberBusinessRules;

        public DeleteBusinessAreaMemberCommandHandler(IMapper mapper,
            IBusinessAreaMemberRepository businessAreaMemberRepository,
            BusinessAreaMemberBusinessRules businessAreaMemberBusinessRules,
            IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _businessAreaMemberRepository = businessAreaMemberRepository;
            _businessAreaMemberBusinessRules = businessAreaMemberBusinessRules;
            _memberRepository = memberRepository;
        }

        public async Task<DeletedBusinessAreaMemberResponse> Handle(DeleteBusinessAreaMemberCommand request,
            CancellationToken cancellationToken)
        {
            Guid MemberId = (await _memberRepository.GetAsync(i => i.UserId == request.UserId)).Id;
            BusinessAreaMember? businessAreaMember = await _businessAreaMemberRepository.GetAsync(
                predicate: bam => bam.Id == request.Id && bam.MemberId == MemberId,
                cancellationToken: cancellationToken);
            await _businessAreaMemberBusinessRules.BusinessAreaMemberShouldExistWhenSelected(
                businessAreaMember);

            await _businessAreaMemberRepository.DeleteAsync(businessAreaMember!, true);

            DeletedBusinessAreaMemberResponse response =
                _mapper.Map<DeletedBusinessAreaMemberResponse>(businessAreaMember);
            return response;
        }
    }
}
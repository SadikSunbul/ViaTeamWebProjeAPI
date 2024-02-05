using Application.Features.BusinessAreaMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.BusinessAreaMembers.Commands.Update;

public class UpdateBusinessAreaMemberCommand : IRequest<UpdatedBusinessAreaMemberResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BusinessAreaId { get; set; }
    public int UserId { get; set; }

    public class UpdateBusinessAreaMemberCommandHandler : IRequestHandler<UpdateBusinessAreaMemberCommand, UpdatedBusinessAreaMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly IBusinessAreaMemberRepository _businessAreaMemberRepository;
        private readonly BusinessAreaMemberBusinessRules _businessAreaMemberBusinessRules;

        public UpdateBusinessAreaMemberCommandHandler(IMapper mapper, IBusinessAreaMemberRepository businessAreaMemberRepository,
                                         BusinessAreaMemberBusinessRules businessAreaMemberBusinessRules, IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _businessAreaMemberRepository = businessAreaMemberRepository;
            _businessAreaMemberBusinessRules = businessAreaMemberBusinessRules;
            _memberRepository = memberRepository;
        }

        public async Task<UpdatedBusinessAreaMemberResponse> Handle(UpdateBusinessAreaMemberCommand request, CancellationToken cancellationToken)
        {
            BusinessAreaMember? businessAreaMember = await _businessAreaMemberRepository.GetAsync(predicate: bam => bam.Id == request.Id, cancellationToken: cancellationToken);
            await _businessAreaMemberBusinessRules.BusinessAreaMemberShouldExistWhenSelected(businessAreaMember);
            businessAreaMember = _mapper.Map(request, businessAreaMember);

            await _businessAreaMemberRepository.UpdateAsync(businessAreaMember!);

            UpdatedBusinessAreaMemberResponse response = _mapper.Map<UpdatedBusinessAreaMemberResponse>(businessAreaMember);
            return response;
        }
    }
}
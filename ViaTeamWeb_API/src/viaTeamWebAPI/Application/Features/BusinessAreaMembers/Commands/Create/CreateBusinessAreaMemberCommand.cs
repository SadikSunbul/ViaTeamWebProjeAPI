using Application.Features.BusinessAreaMembers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Transaction;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.BusinessAreaMembers.Commands.Create;

public class CreateBusinessAreaMemberCommand : IRequest<CreatedBusinessAreaMemberResponse>, ITransactionalRequest
{
    public Guid BusinessAreaId { get; set; }
    public int UserId { get; set; }
    

    public class CreateBusinessAreaMemberCommandHandler : IRequestHandler<CreateBusinessAreaMemberCommand, CreatedBusinessAreaMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessAreaMemberRepository _businessAreaMemberRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly BusinessAreaMemberBusinessRules _businessAreaMemberBusinessRules;

        public CreateBusinessAreaMemberCommandHandler(IMapper mapper, IBusinessAreaMemberRepository businessAreaMemberRepository,
                                         BusinessAreaMemberBusinessRules businessAreaMemberBusinessRules, IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _businessAreaMemberRepository = businessAreaMemberRepository;
            _businessAreaMemberBusinessRules = businessAreaMemberBusinessRules;
            _memberRepository = memberRepository;
        }

        public async Task<CreatedBusinessAreaMemberResponse> Handle(CreateBusinessAreaMemberCommand request, CancellationToken cancellationToken)
        {
            BusinessAreaMember businessAreaMember = new();
            businessAreaMember.MemberId= (await _memberRepository.GetAsync(i => i.UserId == request.UserId)).Id;
            ;
            businessAreaMember.BusinessAreaId = request.BusinessAreaId;

            await _businessAreaMemberRepository.AddAsync(businessAreaMember);

            CreatedBusinessAreaMemberResponse response = _mapper.Map<CreatedBusinessAreaMemberResponse>(businessAreaMember);
            return response;
        }
    }
}
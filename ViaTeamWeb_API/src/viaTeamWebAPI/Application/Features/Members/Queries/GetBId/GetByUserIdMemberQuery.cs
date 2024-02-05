using Application.Features.Members.Queries.GetById;
using Application.Features.Members.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Members.Queries.GetBId;

public class GetByUserIdMemberQuery: IRequest<GetByIdMemberResponse>
{
    public Guid Id { get; set; }

    public class GetByIdMemberQueryHandler : IRequestHandler<GetByUserIdMemberQuery, GetByIdMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly MemberBusinessRules _memberBusinessRules;

        public GetByIdMemberQueryHandler(IMapper mapper, IMemberRepository memberRepository, MemberBusinessRules memberBusinessRules)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _memberBusinessRules = memberBusinessRules;
        }

        public async Task<GetByIdMemberResponse> Handle(GetByUserIdMemberQuery request, CancellationToken cancellationToken)
        {
            Member? member = await _memberRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _memberBusinessRules.MemberShouldExistWhenSelected(member);

            GetByIdMemberResponse response = _mapper.Map<GetByIdMemberResponse>(member);
            return response;
        }
    }
}
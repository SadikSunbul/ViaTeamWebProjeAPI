using Application.Features.Members.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;

namespace Application.Features.Members.Commands.Update;

public class UpdateMemberCommand : IRequest<UpdatedMemberResponse>
{
    public int UserId { get; set; }
    public string Job { get; set; }
    public string Country { get; set; }
    public string Authirize { get; set; }
    
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, UpdatedMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly MemberBusinessRules _memberBusinessRules;

        public UpdateMemberCommandHandler(IMapper mapper, IMemberRepository memberRepository,
                                         MemberBusinessRules memberBusinessRules)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _memberBusinessRules = memberBusinessRules;
        }

        public async Task<UpdatedMemberResponse> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            Member? member = await _memberRepository.GetAsync(predicate: m => m.UserId == request.UserId, cancellationToken: cancellationToken);
            await _memberBusinessRules.MemberShouldExistWhenSelected(member);
            member = _mapper.Map(request, member);

            await _memberRepository.UpdateAsync(member!);

            UpdatedMemberResponse response = _mapper.Map<UpdatedMemberResponse>(member);
            return response;
        }
    }
}
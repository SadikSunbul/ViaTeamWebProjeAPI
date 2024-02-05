using Application.Features.Members.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;

namespace Application.Features.Members.Commands.Create;

public class CreateMemberCommand : IRequest<CreatedMemberResponse>
{
    public int UserId { get; set; }
    public string Job { get; set; }
    public string Country { get; set; }
    public string Authirize { get; set; }
    public TeamMember TeamMember { get; set; }
    public User User { get; set; }

    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, CreatedMemberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly MemberBusinessRules _memberBusinessRules;

        public CreateMemberCommandHandler(IMapper mapper, IMemberRepository memberRepository,
                                         MemberBusinessRules memberBusinessRules)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _memberBusinessRules = memberBusinessRules;
        }

        public async Task<CreatedMemberResponse> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            Member member = _mapper.Map<Member>(request);

            await _memberRepository.AddAsync(member);

            CreatedMemberResponse response = _mapper.Map<CreatedMemberResponse>(member);
            return response;
        }
    }
}
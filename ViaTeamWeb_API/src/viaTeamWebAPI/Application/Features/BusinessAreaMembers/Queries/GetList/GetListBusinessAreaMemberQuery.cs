using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.BusinessAreaMembers.Queries.GetList;

public class GetListBusinessAreaMemberQuery : IRequest<GetListResponse<GetListBusinessAreaMemberListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public int UserId { get; set; }

    public class GetListBusinessAreaMemberQueryHandler : IRequestHandler<GetListBusinessAreaMemberQuery, GetListResponse<GetListBusinessAreaMemberListItemDto>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IBusinessAreaMemberRepository _businessAreaMemberRepository;
        private readonly IMapper _mapper;

        public GetListBusinessAreaMemberQueryHandler(IBusinessAreaMemberRepository businessAreaMemberRepository, IMapper mapper, IMemberRepository memberRepository)
        {
            _businessAreaMemberRepository = businessAreaMemberRepository;
            _mapper = mapper;
            _memberRepository = memberRepository;
        }

        public async Task<GetListResponse<GetListBusinessAreaMemberListItemDto>> Handle(GetListBusinessAreaMemberQuery request, CancellationToken cancellationToken)
        {
            Guid MemeberId=(await _memberRepository.GetAsync(i => i.UserId == request.UserId)).Id;
            IPaginate<BusinessAreaMember> businessAreaMembers = await _businessAreaMemberRepository.GetListAsync(
                predicate:i=>i.MemberId==MemeberId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListBusinessAreaMemberListItemDto> response = _mapper.Map<GetListResponse<GetListBusinessAreaMemberListItemDto>>(businessAreaMembers);
            return response;
        }
    }
}
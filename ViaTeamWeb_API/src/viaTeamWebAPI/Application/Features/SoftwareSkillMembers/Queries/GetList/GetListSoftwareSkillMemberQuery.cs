using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.SoftwareSkillMembers.Queries.GetList;

public class GetListSoftwareSkillMemberQuery : IRequest<GetListResponse<GetListSoftwareSkillMemberListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public Guid MemberId { get; set; }

    public class GetListSoftwareSkillMemberQueryHandler : IRequestHandler<GetListSoftwareSkillMemberQuery, GetListResponse<GetListSoftwareSkillMemberListItemDto>>
    {
        private readonly ISoftwareSkillMemberRepository _softwareSkillMemberRepository;
        private readonly IMapper _mapper;

        public GetListSoftwareSkillMemberQueryHandler(ISoftwareSkillMemberRepository softwareSkillMemberRepository, IMapper mapper)
        {
            _softwareSkillMemberRepository = softwareSkillMemberRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSoftwareSkillMemberListItemDto>> Handle(GetListSoftwareSkillMemberQuery request, CancellationToken cancellationToken)
        {
            IPaginate<SoftwareSkillMember> softwareSkillMembers = await _softwareSkillMemberRepository.GetListAsync(
                predicate:i=>i.MemberId==request.MemberId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSoftwareSkillMemberListItemDto> response = _mapper.Map<GetListResponse<GetListSoftwareSkillMemberListItemDto>>(softwareSkillMembers);
            return response;
        }
    }
}
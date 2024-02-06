using Application.Features.SoftwareSkills.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.SoftwareSkills.Constants.SoftwareSkillsOperationClaims;

namespace Application.Features.SoftwareSkills.Queries.GetList;

public class GetListSoftwareSkillQuery : IRequest<GetListResponse<GetListSoftwareSkillListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    // public string[] Roles => new[] { Admin, Read };

    public class GetListSoftwareSkillQueryHandler : IRequestHandler<GetListSoftwareSkillQuery, GetListResponse<GetListSoftwareSkillListItemDto>>
    {
        private readonly ISoftwareSkillRepository _softwareSkillRepository;
        private readonly IMapper _mapper;

        public GetListSoftwareSkillQueryHandler(ISoftwareSkillRepository softwareSkillRepository, IMapper mapper)
        {
            _softwareSkillRepository = softwareSkillRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSoftwareSkillListItemDto>> Handle(GetListSoftwareSkillQuery request, CancellationToken cancellationToken)
        {
            IPaginate<SoftwareSkill> softwareSkills = await _softwareSkillRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSoftwareSkillListItemDto> response = _mapper.Map<GetListResponse<GetListSoftwareSkillListItemDto>>(softwareSkills);
            return response;
        }
    }
}
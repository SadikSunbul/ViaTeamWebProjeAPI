using Application.Features.BusinessAreas.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.BusinessAreas.Constants.BusinessAreasOperationClaims;

namespace Application.Features.BusinessAreas.Queries.GetList;

public class GetListBusinessAreaQuery : IRequest<GetListResponse<GetListBusinessAreaListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListBusinessAreaQueryHandler : IRequestHandler<GetListBusinessAreaQuery, GetListResponse<GetListBusinessAreaListItemDto>>
    {
        private readonly IBusinessAreaRepository _businessAreaRepository;
        private readonly IMapper _mapper;

        public GetListBusinessAreaQueryHandler(IBusinessAreaRepository businessAreaRepository, IMapper mapper)
        {
            _businessAreaRepository = businessAreaRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBusinessAreaListItemDto>> Handle(GetListBusinessAreaQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BusinessArea> businessAreas = await _businessAreaRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListBusinessAreaListItemDto> response = _mapper.Map<GetListResponse<GetListBusinessAreaListItemDto>>(businessAreas);
            return response;
        }
    }
}
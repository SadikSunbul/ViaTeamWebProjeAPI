using Application.Features.Alooos.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Alooos.Constants.AlooosOperationClaims;

namespace Application.Features.Alooos.Queries.GetList;

public class GetListAloooQuery : IRequest<GetListResponse<GetListAloooListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListAlooos({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetAlooos";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAloooQueryHandler : IRequestHandler<GetListAloooQuery, GetListResponse<GetListAloooListItemDto>>
    {
        private readonly IAloooRepository _aloooRepository;
        private readonly IMapper _mapper;

        public GetListAloooQueryHandler(IAloooRepository aloooRepository, IMapper mapper)
        {
            _aloooRepository = aloooRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAloooListItemDto>> Handle(GetListAloooQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Alooo> alooos = await _aloooRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAloooListItemDto> response = _mapper.Map<GetListResponse<GetListAloooListItemDto>>(alooos);
            return response;
        }
    }
}
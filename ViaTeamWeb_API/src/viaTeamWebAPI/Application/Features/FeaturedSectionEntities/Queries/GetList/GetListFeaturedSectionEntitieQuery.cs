using Application.Features.FeaturedSectionEntities.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.FeaturedSectionEntities.Constants.FeaturedSectionEntitiesOperationClaims;

namespace Application.Features.FeaturedSectionEntities.Queries.GetList;

public class GetListFeaturedSectionEntitieQuery : IRequest<GetListResponse<GetListFeaturedSectionEntitieListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListFeaturedSectionEntitieQueryHandler : IRequestHandler<GetListFeaturedSectionEntitieQuery, GetListResponse<GetListFeaturedSectionEntitieListItemDto>>
    {
        private readonly IFeaturedSectionEntitieRepository _featuredSectionEntitieRepository;
        private readonly IMapper _mapper;

        public GetListFeaturedSectionEntitieQueryHandler(IFeaturedSectionEntitieRepository featuredSectionEntitieRepository, IMapper mapper)
        {
            _featuredSectionEntitieRepository = featuredSectionEntitieRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFeaturedSectionEntitieListItemDto>> Handle(GetListFeaturedSectionEntitieQuery request, CancellationToken cancellationToken)
        {
            IPaginate<FeaturedSectionEntitie> featuredSectionEntities = await _featuredSectionEntitieRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFeaturedSectionEntitieListItemDto> response = _mapper.Map<GetListResponse<GetListFeaturedSectionEntitieListItemDto>>(featuredSectionEntities);
            return response;
        }
    }
}
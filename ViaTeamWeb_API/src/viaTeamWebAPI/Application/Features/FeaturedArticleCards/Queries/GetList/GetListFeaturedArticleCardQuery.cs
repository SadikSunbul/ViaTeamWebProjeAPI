using Application.Features.FeaturedArticleCards.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.FeaturedArticleCards.Constants.FeaturedArticleCardsOperationClaims;

namespace Application.Features.FeaturedArticleCards.Queries.GetList;

public class GetListFeaturedArticleCardQuery : IRequest<GetListResponse<GetListFeaturedArticleCardListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListFeaturedArticleCardQueryHandler : IRequestHandler<GetListFeaturedArticleCardQuery, GetListResponse<GetListFeaturedArticleCardListItemDto>>
    {
        private readonly IFeaturedArticleCardRepository _featuredArticleCardRepository;
        private readonly IMapper _mapper;

        public GetListFeaturedArticleCardQueryHandler(IFeaturedArticleCardRepository featuredArticleCardRepository, IMapper mapper)
        {
            _featuredArticleCardRepository = featuredArticleCardRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFeaturedArticleCardListItemDto>> Handle(GetListFeaturedArticleCardQuery request, CancellationToken cancellationToken)
        {
            IPaginate<FeaturedArticleCard> featuredArticleCards = await _featuredArticleCardRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFeaturedArticleCardListItemDto> response = _mapper.Map<GetListResponse<GetListFeaturedArticleCardListItemDto>>(featuredArticleCards);
            return response;
        }
    }
}
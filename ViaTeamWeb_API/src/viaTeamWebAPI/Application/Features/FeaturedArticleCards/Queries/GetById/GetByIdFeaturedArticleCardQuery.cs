using Application.Features.FeaturedArticleCards.Constants;
using Application.Features.FeaturedArticleCards.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.FeaturedArticleCards.Constants.FeaturedArticleCardsOperationClaims;

namespace Application.Features.FeaturedArticleCards.Queries.GetById;

public class GetByIdFeaturedArticleCardQuery : IRequest<GetByIdFeaturedArticleCardResponse>
{
    public Guid Id { get; set; }

    // public string[] Roles => new[] { Admin, Read };

    public class GetByIdFeaturedArticleCardQueryHandler : IRequestHandler<GetByIdFeaturedArticleCardQuery, GetByIdFeaturedArticleCardResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeaturedArticleCardRepository _featuredArticleCardRepository;
        private readonly FeaturedArticleCardBusinessRules _featuredArticleCardBusinessRules;

        public GetByIdFeaturedArticleCardQueryHandler(IMapper mapper, IFeaturedArticleCardRepository featuredArticleCardRepository, FeaturedArticleCardBusinessRules featuredArticleCardBusinessRules)
        {
            _mapper = mapper;
            _featuredArticleCardRepository = featuredArticleCardRepository;
            _featuredArticleCardBusinessRules = featuredArticleCardBusinessRules;
        }

        public async Task<GetByIdFeaturedArticleCardResponse> Handle(GetByIdFeaturedArticleCardQuery request, CancellationToken cancellationToken)
        {
            FeaturedArticleCard? featuredArticleCard = await _featuredArticleCardRepository.GetAsync(predicate: fac => fac.Id == request.Id, cancellationToken: cancellationToken);
            await _featuredArticleCardBusinessRules.FeaturedArticleCardShouldExistWhenSelected(featuredArticleCard);

            GetByIdFeaturedArticleCardResponse response = _mapper.Map<GetByIdFeaturedArticleCardResponse>(featuredArticleCard);
            return response;
        }
    }
}
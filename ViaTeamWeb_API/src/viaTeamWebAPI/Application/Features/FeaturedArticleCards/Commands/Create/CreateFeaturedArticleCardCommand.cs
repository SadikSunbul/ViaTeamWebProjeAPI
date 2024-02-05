using Application.Features.FeaturedArticleCards.Constants;
using Application.Features.FeaturedArticleCards.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.FeaturedArticleCards.Constants.FeaturedArticleCardsOperationClaims;

namespace Application.Features.FeaturedArticleCards.Commands.Create;

public class CreateFeaturedArticleCardCommand : IRequest<CreatedFeaturedArticleCardResponse>, ISecuredRequest
{
    public string Title { get; set; }
    public string Explanation { get; set; }
    public string Writer { get; set; }
    public Guid FeaturedSectionEntitiesId { get; set; }

    public string[] Roles => new[] { Admin, Write, FeaturedArticleCardsOperationClaims.Create };

    public class CreateFeaturedArticleCardCommandHandler : IRequestHandler<CreateFeaturedArticleCardCommand, CreatedFeaturedArticleCardResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeaturedArticleCardRepository _featuredArticleCardRepository;
        private readonly FeaturedArticleCardBusinessRules _featuredArticleCardBusinessRules;

        public CreateFeaturedArticleCardCommandHandler(IMapper mapper, IFeaturedArticleCardRepository featuredArticleCardRepository,
                                         FeaturedArticleCardBusinessRules featuredArticleCardBusinessRules)
        {
            _mapper = mapper;
            _featuredArticleCardRepository = featuredArticleCardRepository;
            _featuredArticleCardBusinessRules = featuredArticleCardBusinessRules;
        }

        public async Task<CreatedFeaturedArticleCardResponse> Handle(CreateFeaturedArticleCardCommand request, CancellationToken cancellationToken)
        {
            FeaturedArticleCard featuredArticleCard = _mapper.Map<FeaturedArticleCard>(request);

            await _featuredArticleCardRepository.AddAsync(featuredArticleCard);

            CreatedFeaturedArticleCardResponse response = _mapper.Map<CreatedFeaturedArticleCardResponse>(featuredArticleCard);
            return response;
        }
    }
}
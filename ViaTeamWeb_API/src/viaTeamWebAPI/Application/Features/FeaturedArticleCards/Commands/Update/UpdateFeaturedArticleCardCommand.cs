using Application.Features.FeaturedArticleCards.Constants;
using Application.Features.FeaturedArticleCards.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.FeaturedArticleCards.Constants.FeaturedArticleCardsOperationClaims;

namespace Application.Features.FeaturedArticleCards.Commands.Update;

public class UpdateFeaturedArticleCardCommand : IRequest<UpdatedFeaturedArticleCardResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Explanation { get; set; }
    public string Writer { get; set; }
    public Guid FeaturedSectionEntitiesId { get; set; }

    public string[] Roles => new[] { Admin, Write, FeaturedArticleCardsOperationClaims.Update };

    public class UpdateFeaturedArticleCardCommandHandler : IRequestHandler<UpdateFeaturedArticleCardCommand, UpdatedFeaturedArticleCardResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeaturedArticleCardRepository _featuredArticleCardRepository;
        private readonly FeaturedArticleCardBusinessRules _featuredArticleCardBusinessRules;

        public UpdateFeaturedArticleCardCommandHandler(IMapper mapper, IFeaturedArticleCardRepository featuredArticleCardRepository,
                                         FeaturedArticleCardBusinessRules featuredArticleCardBusinessRules)
        {
            _mapper = mapper;
            _featuredArticleCardRepository = featuredArticleCardRepository;
            _featuredArticleCardBusinessRules = featuredArticleCardBusinessRules;
        }

        public async Task<UpdatedFeaturedArticleCardResponse> Handle(UpdateFeaturedArticleCardCommand request, CancellationToken cancellationToken)
        {
            FeaturedArticleCard? featuredArticleCard = await _featuredArticleCardRepository.GetAsync(predicate: fac => fac.Id == request.Id, cancellationToken: cancellationToken);
            await _featuredArticleCardBusinessRules.FeaturedArticleCardShouldExistWhenSelected(featuredArticleCard);
            featuredArticleCard = _mapper.Map(request, featuredArticleCard);

            await _featuredArticleCardRepository.UpdateAsync(featuredArticleCard!);

            UpdatedFeaturedArticleCardResponse response = _mapper.Map<UpdatedFeaturedArticleCardResponse>(featuredArticleCard);
            return response;
        }
    }
}
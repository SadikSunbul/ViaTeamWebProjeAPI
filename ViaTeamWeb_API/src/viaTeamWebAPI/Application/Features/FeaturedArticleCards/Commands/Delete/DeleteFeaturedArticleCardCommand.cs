using Application.Features.FeaturedArticleCards.Constants;
using Application.Features.FeaturedArticleCards.Constants;
using Application.Features.FeaturedArticleCards.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.FeaturedArticleCards.Constants.FeaturedArticleCardsOperationClaims;

namespace Application.Features.FeaturedArticleCards.Commands.Delete;

public class DeleteFeaturedArticleCardCommand : IRequest<DeletedFeaturedArticleCardResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, FeaturedArticleCardsOperationClaims.Delete };

    public class DeleteFeaturedArticleCardCommandHandler : IRequestHandler<DeleteFeaturedArticleCardCommand, DeletedFeaturedArticleCardResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeaturedArticleCardRepository _featuredArticleCardRepository;
        private readonly FeaturedArticleCardBusinessRules _featuredArticleCardBusinessRules;

        public DeleteFeaturedArticleCardCommandHandler(IMapper mapper, IFeaturedArticleCardRepository featuredArticleCardRepository,
                                         FeaturedArticleCardBusinessRules featuredArticleCardBusinessRules)
        {
            _mapper = mapper;
            _featuredArticleCardRepository = featuredArticleCardRepository;
            _featuredArticleCardBusinessRules = featuredArticleCardBusinessRules;
        }

        public async Task<DeletedFeaturedArticleCardResponse> Handle(DeleteFeaturedArticleCardCommand request, CancellationToken cancellationToken)
        {
            FeaturedArticleCard? featuredArticleCard = await _featuredArticleCardRepository.GetAsync(predicate: fac => fac.Id == request.Id, cancellationToken: cancellationToken);
            await _featuredArticleCardBusinessRules.FeaturedArticleCardShouldExistWhenSelected(featuredArticleCard);

            await _featuredArticleCardRepository.DeleteAsync(featuredArticleCard!,true);

            DeletedFeaturedArticleCardResponse response = _mapper.Map<DeletedFeaturedArticleCardResponse>(featuredArticleCard);
            return response;
        }
    }
}
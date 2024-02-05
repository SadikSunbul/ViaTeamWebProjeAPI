using Application.Features.FeaturedSectionEntities.Constants;
using Application.Features.FeaturedSectionEntities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.FeaturedSectionEntities.Constants.FeaturedSectionEntitiesOperationClaims;

namespace Application.Features.FeaturedSectionEntities.Commands.Create;

public class CreateFeaturedSectionEntitieCommand : IRequest<CreatedFeaturedSectionEntitieResponse>, ISecuredRequest
{
    public string SmallTitle { get; set; }
    public string Title { get; set; }

    public string[] Roles => new[] { Admin, Write, FeaturedSectionEntitiesOperationClaims.Create };

    public class CreateFeaturedSectionEntitieCommandHandler : IRequestHandler<CreateFeaturedSectionEntitieCommand, CreatedFeaturedSectionEntitieResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeaturedSectionEntitieRepository _featuredSectionEntitieRepository;
        private readonly FeaturedSectionEntitieBusinessRules _featuredSectionEntitieBusinessRules;

        public CreateFeaturedSectionEntitieCommandHandler(IMapper mapper, IFeaturedSectionEntitieRepository featuredSectionEntitieRepository,
                                         FeaturedSectionEntitieBusinessRules featuredSectionEntitieBusinessRules)
        {
            _mapper = mapper;
            _featuredSectionEntitieRepository = featuredSectionEntitieRepository;
            _featuredSectionEntitieBusinessRules = featuredSectionEntitieBusinessRules;
        }

        public async Task<CreatedFeaturedSectionEntitieResponse> Handle(CreateFeaturedSectionEntitieCommand request, CancellationToken cancellationToken)
        {
            FeaturedSectionEntitie featuredSectionEntitie = _mapper.Map<FeaturedSectionEntitie>(request);

            await _featuredSectionEntitieRepository.AddAsync(featuredSectionEntitie);

            CreatedFeaturedSectionEntitieResponse response = _mapper.Map<CreatedFeaturedSectionEntitieResponse>(featuredSectionEntitie);
            return response;
        }
    }
}
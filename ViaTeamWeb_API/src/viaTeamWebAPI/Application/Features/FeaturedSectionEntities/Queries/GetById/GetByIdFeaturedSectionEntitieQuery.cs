using Application.Features.FeaturedSectionEntities.Constants;
using Application.Features.FeaturedSectionEntities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.FeaturedSectionEntities.Constants.FeaturedSectionEntitiesOperationClaims;

namespace Application.Features.FeaturedSectionEntities.Queries.GetById;

public class GetByIdFeaturedSectionEntitieQuery : IRequest<GetByIdFeaturedSectionEntitieResponse>
{
    public Guid Id { get; set; }

    // public string[] Roles => new[] { Admin, Read };

    public class GetByIdFeaturedSectionEntitieQueryHandler : IRequestHandler<GetByIdFeaturedSectionEntitieQuery, GetByIdFeaturedSectionEntitieResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeaturedSectionEntitieRepository _featuredSectionEntitieRepository;
        private readonly FeaturedSectionEntitieBusinessRules _featuredSectionEntitieBusinessRules;

        public GetByIdFeaturedSectionEntitieQueryHandler(IMapper mapper, IFeaturedSectionEntitieRepository featuredSectionEntitieRepository, FeaturedSectionEntitieBusinessRules featuredSectionEntitieBusinessRules)
        {
            _mapper = mapper;
            _featuredSectionEntitieRepository = featuredSectionEntitieRepository;
            _featuredSectionEntitieBusinessRules = featuredSectionEntitieBusinessRules;
        }

        public async Task<GetByIdFeaturedSectionEntitieResponse> Handle(GetByIdFeaturedSectionEntitieQuery request, CancellationToken cancellationToken)
        {
            FeaturedSectionEntitie? featuredSectionEntitie = await _featuredSectionEntitieRepository.GetAsync(predicate: fse => fse.Id == request.Id, cancellationToken: cancellationToken);
            await _featuredSectionEntitieBusinessRules.FeaturedSectionEntitieShouldExistWhenSelected(featuredSectionEntitie);

            GetByIdFeaturedSectionEntitieResponse response = _mapper.Map<GetByIdFeaturedSectionEntitieResponse>(featuredSectionEntitie);
            return response;
        }
    }
}
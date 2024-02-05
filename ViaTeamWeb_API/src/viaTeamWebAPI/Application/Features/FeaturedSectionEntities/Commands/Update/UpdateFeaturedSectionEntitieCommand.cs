using Application.Features.FeaturedSectionEntities.Constants;
using Application.Features.FeaturedSectionEntities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.FeaturedSectionEntities.Constants.FeaturedSectionEntitiesOperationClaims;

namespace Application.Features.FeaturedSectionEntities.Commands.Update;

public class UpdateFeaturedSectionEntitieCommand : IRequest<UpdatedFeaturedSectionEntitieResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string SmallTitle { get; set; }
    public string Title { get; set; }

    public string[] Roles => new[] { Admin, Write, FeaturedSectionEntitiesOperationClaims.Update };

    public class UpdateFeaturedSectionEntitieCommandHandler : IRequestHandler<UpdateFeaturedSectionEntitieCommand, UpdatedFeaturedSectionEntitieResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeaturedSectionEntitieRepository _featuredSectionEntitieRepository;
        private readonly FeaturedSectionEntitieBusinessRules _featuredSectionEntitieBusinessRules;

        public UpdateFeaturedSectionEntitieCommandHandler(IMapper mapper, IFeaturedSectionEntitieRepository featuredSectionEntitieRepository,
                                         FeaturedSectionEntitieBusinessRules featuredSectionEntitieBusinessRules)
        {
            _mapper = mapper;
            _featuredSectionEntitieRepository = featuredSectionEntitieRepository;
            _featuredSectionEntitieBusinessRules = featuredSectionEntitieBusinessRules;
        }

        public async Task<UpdatedFeaturedSectionEntitieResponse> Handle(UpdateFeaturedSectionEntitieCommand request, CancellationToken cancellationToken)
        {
            FeaturedSectionEntitie? featuredSectionEntitie = await _featuredSectionEntitieRepository.GetAsync(predicate: fse => fse.Id == request.Id, cancellationToken: cancellationToken);
            await _featuredSectionEntitieBusinessRules.FeaturedSectionEntitieShouldExistWhenSelected(featuredSectionEntitie);
            featuredSectionEntitie = _mapper.Map(request, featuredSectionEntitie);

            await _featuredSectionEntitieRepository.UpdateAsync(featuredSectionEntitie!);

            UpdatedFeaturedSectionEntitieResponse response = _mapper.Map<UpdatedFeaturedSectionEntitieResponse>(featuredSectionEntitie);
            return response;
        }
    }
}
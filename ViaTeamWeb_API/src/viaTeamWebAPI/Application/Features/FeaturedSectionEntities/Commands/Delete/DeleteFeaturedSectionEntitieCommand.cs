using Application.Features.FeaturedSectionEntities.Constants;
using Application.Features.FeaturedSectionEntities.Constants;
using Application.Features.FeaturedSectionEntities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.FeaturedSectionEntities.Constants.FeaturedSectionEntitiesOperationClaims;

namespace Application.Features.FeaturedSectionEntities.Commands.Delete;

public class DeleteFeaturedSectionEntitieCommand : IRequest<DeletedFeaturedSectionEntitieResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, FeaturedSectionEntitiesOperationClaims.Delete };

    public class DeleteFeaturedSectionEntitieCommandHandler : IRequestHandler<DeleteFeaturedSectionEntitieCommand, DeletedFeaturedSectionEntitieResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeaturedSectionEntitieRepository _featuredSectionEntitieRepository;
        private readonly FeaturedSectionEntitieBusinessRules _featuredSectionEntitieBusinessRules;

        public DeleteFeaturedSectionEntitieCommandHandler(IMapper mapper, IFeaturedSectionEntitieRepository featuredSectionEntitieRepository,
                                         FeaturedSectionEntitieBusinessRules featuredSectionEntitieBusinessRules)
        {
            _mapper = mapper;
            _featuredSectionEntitieRepository = featuredSectionEntitieRepository;
            _featuredSectionEntitieBusinessRules = featuredSectionEntitieBusinessRules;
        }

        public async Task<DeletedFeaturedSectionEntitieResponse> Handle(DeleteFeaturedSectionEntitieCommand request, CancellationToken cancellationToken)
        {
            FeaturedSectionEntitie? featuredSectionEntitie = await _featuredSectionEntitieRepository.GetAsync(predicate: fse => fse.Id == request.Id, cancellationToken: cancellationToken);
            await _featuredSectionEntitieBusinessRules.FeaturedSectionEntitieShouldExistWhenSelected(featuredSectionEntitie);

            await _featuredSectionEntitieRepository.DeleteAsync(featuredSectionEntitie!,true);

            DeletedFeaturedSectionEntitieResponse response = _mapper.Map<DeletedFeaturedSectionEntitieResponse>(featuredSectionEntitie);
            return response;
        }
    }
}
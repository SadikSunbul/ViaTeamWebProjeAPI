using Application.Features.HeroSectionWrites.Constants;
using Application.Features.HeroSectionWrites.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.HeroSectionWrites.Constants.HeroSectionWritesOperationClaims;

namespace Application.Features.HeroSectionWrites.Commands.Update;

public class UpdateHeroSectionWriteCommand : IRequest<UpdatedHeroSectionWriteResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Paragraph { get; set; }
    public string ButtonText { get; set; }

    public string[] Roles => new[] { Admin, Write, HeroSectionWritesOperationClaims.Update };

    public class UpdateHeroSectionWriteCommandHandler : IRequestHandler<UpdateHeroSectionWriteCommand, UpdatedHeroSectionWriteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHeroSectionWriteRepository _heroSectionWriteRepository;
        private readonly HeroSectionWriteBusinessRules _heroSectionWriteBusinessRules;

        public UpdateHeroSectionWriteCommandHandler(IMapper mapper, IHeroSectionWriteRepository heroSectionWriteRepository,
                                         HeroSectionWriteBusinessRules heroSectionWriteBusinessRules)
        {
            _mapper = mapper;
            _heroSectionWriteRepository = heroSectionWriteRepository;
            _heroSectionWriteBusinessRules = heroSectionWriteBusinessRules;
        }

        public async Task<UpdatedHeroSectionWriteResponse> Handle(UpdateHeroSectionWriteCommand request, CancellationToken cancellationToken)
        {
            HeroSectionWrite? heroSectionWrite = await _heroSectionWriteRepository.GetAsync(predicate: hsw => hsw.Id == request.Id, cancellationToken: cancellationToken);
            await _heroSectionWriteBusinessRules.HeroSectionWriteShouldExistWhenSelected(heroSectionWrite);
            heroSectionWrite = _mapper.Map(request, heroSectionWrite);

            await _heroSectionWriteRepository.UpdateAsync(heroSectionWrite!);

            UpdatedHeroSectionWriteResponse response = _mapper.Map<UpdatedHeroSectionWriteResponse>(heroSectionWrite);
            return response;
        }
    }
}
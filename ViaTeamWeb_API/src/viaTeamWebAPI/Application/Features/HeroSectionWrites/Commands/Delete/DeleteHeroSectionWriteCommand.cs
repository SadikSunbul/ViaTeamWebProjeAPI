using Application.Features.HeroSectionWrites.Constants;
using Application.Features.HeroSectionWrites.Constants;
using Application.Features.HeroSectionWrites.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.HeroSectionWrites.Constants.HeroSectionWritesOperationClaims;

namespace Application.Features.HeroSectionWrites.Commands.Delete;

public class DeleteHeroSectionWriteCommand : IRequest<DeletedHeroSectionWriteResponse>, ISecuredRequest,
    ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, HeroSectionWritesOperationClaims.Delete };

    public class DeleteHeroSectionWriteCommandHandler : IRequestHandler<DeleteHeroSectionWriteCommand,
        DeletedHeroSectionWriteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHeroSectionWriteRepository _heroSectionWriteRepository;
        private readonly HeroSectionWriteBusinessRules _heroSectionWriteBusinessRules;

        public DeleteHeroSectionWriteCommandHandler(IMapper mapper,
            IHeroSectionWriteRepository heroSectionWriteRepository,
            HeroSectionWriteBusinessRules heroSectionWriteBusinessRules)
        {
            _mapper = mapper;
            _heroSectionWriteRepository = heroSectionWriteRepository;
            _heroSectionWriteBusinessRules = heroSectionWriteBusinessRules;
        }

        public async Task<DeletedHeroSectionWriteResponse> Handle(DeleteHeroSectionWriteCommand request,
            CancellationToken cancellationToken)
        {
            HeroSectionWrite? heroSectionWrite =
                await _heroSectionWriteRepository.GetAsync(predicate: hsw => hsw.Id == request.Id,
                    cancellationToken: cancellationToken);
            await _heroSectionWriteBusinessRules.HeroSectionWriteShouldExistWhenSelected(heroSectionWrite);

            await _heroSectionWriteRepository.DeleteAsync(heroSectionWrite!,true);

            DeletedHeroSectionWriteResponse response =
                _mapper.Map<DeletedHeroSectionWriteResponse>(heroSectionWrite);
            return response;
        }
    }
}
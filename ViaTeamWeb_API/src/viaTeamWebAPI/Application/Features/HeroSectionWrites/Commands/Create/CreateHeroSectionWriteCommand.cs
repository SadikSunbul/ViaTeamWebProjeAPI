using Application.Features.HeroSectionWrites.Constants;
using Application.Features.HeroSectionWrites.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.HeroSectionWrites.Constants.HeroSectionWritesOperationClaims;

namespace Application.Features.HeroSectionWrites.Commands.Create;

public class CreateHeroSectionWriteCommand : IRequest<CreatedHeroSectionWriteResponse>, ISecuredRequest, ITransactionalRequest
{
    public string Title { get; set; }
    public string Paragraph { get; set; }
    public string ButtonText { get; set; }

    public string[] Roles => new[] { Admin, Write, HeroSectionWritesOperationClaims.Create };

    public class CreateHeroSectionWriteCommandHandler : IRequestHandler<CreateHeroSectionWriteCommand, CreatedHeroSectionWriteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHeroSectionWriteRepository _heroSectionWriteRepository;
        private readonly HeroSectionWriteBusinessRules _heroSectionWriteBusinessRules;

        public CreateHeroSectionWriteCommandHandler(IMapper mapper, IHeroSectionWriteRepository heroSectionWriteRepository,
                                         HeroSectionWriteBusinessRules heroSectionWriteBusinessRules)
        {
            _mapper = mapper;
            _heroSectionWriteRepository = heroSectionWriteRepository;
            _heroSectionWriteBusinessRules = heroSectionWriteBusinessRules;
        }

        public async Task<CreatedHeroSectionWriteResponse> Handle(CreateHeroSectionWriteCommand request, CancellationToken cancellationToken)
        {
            HeroSectionWrite heroSectionWrite = _mapper.Map<HeroSectionWrite>(request);

            await _heroSectionWriteRepository.AddAsync(heroSectionWrite);

            CreatedHeroSectionWriteResponse response = _mapper.Map<CreatedHeroSectionWriteResponse>(heroSectionWrite);
            return response;
        }
    }
}
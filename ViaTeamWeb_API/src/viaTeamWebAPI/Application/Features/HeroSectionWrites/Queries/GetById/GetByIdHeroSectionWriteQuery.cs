using Application.Features.HeroSectionWrites.Constants;
using Application.Features.HeroSectionWrites.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.HeroSectionWrites.Constants.HeroSectionWritesOperationClaims;

namespace Application.Features.HeroSectionWrites.Queries.GetById;

public class GetByIdHeroSectionWriteQuery : IRequest<GetByIdHeroSectionWriteResponse>
{
    public Guid Id { get; set; }

    // public string[] Roles => new[] { Admin, Read };

    public class GetByIdHeroSectionWriteQueryHandler : IRequestHandler<GetByIdHeroSectionWriteQuery, GetByIdHeroSectionWriteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHeroSectionWriteRepository _heroSectionWriteRepository;
        private readonly HeroSectionWriteBusinessRules _heroSectionWriteBusinessRules;

        public GetByIdHeroSectionWriteQueryHandler(IMapper mapper, IHeroSectionWriteRepository heroSectionWriteRepository, HeroSectionWriteBusinessRules heroSectionWriteBusinessRules)
        {
            _mapper = mapper;
            _heroSectionWriteRepository = heroSectionWriteRepository;
            _heroSectionWriteBusinessRules = heroSectionWriteBusinessRules;
        }

        public async Task<GetByIdHeroSectionWriteResponse> Handle(GetByIdHeroSectionWriteQuery request, CancellationToken cancellationToken)
        {
            HeroSectionWrite? heroSectionWrite = await _heroSectionWriteRepository.GetAsync(predicate: hsw => hsw.Id == request.Id, cancellationToken: cancellationToken);
            await _heroSectionWriteBusinessRules.HeroSectionWriteShouldExistWhenSelected(heroSectionWrite);

            GetByIdHeroSectionWriteResponse response = _mapper.Map<GetByIdHeroSectionWriteResponse>(heroSectionWrite);
            return response;
        }
    }
}
using Application.Features.ExternalLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ExternalLinks.Queries.GetById;

public class GetByIdExternalLinkQuery : IRequest<GetByIdExternalLinkResponse>
{
    public Guid Id { get; set; }

    public class GetByIdExternalLinkQueryHandler : IRequestHandler<GetByIdExternalLinkQuery, GetByIdExternalLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly IExternalLinkRepository _externalLinkRepository;
        private readonly ExternalLinkBusinessRules _externalLinkBusinessRules;

        public GetByIdExternalLinkQueryHandler(IMapper mapper, IExternalLinkRepository externalLinkRepository, ExternalLinkBusinessRules externalLinkBusinessRules)
        {
            _mapper = mapper;
            _externalLinkRepository = externalLinkRepository;
            _externalLinkBusinessRules = externalLinkBusinessRules;
        }

        public async Task<GetByIdExternalLinkResponse> Handle(GetByIdExternalLinkQuery request, CancellationToken cancellationToken)
        {
            ExternalLink? externalLink = await _externalLinkRepository.GetAsync(predicate: el => el.Id == request.Id, cancellationToken: cancellationToken);
            await _externalLinkBusinessRules.ExternalLinkShouldExistWhenSelected(externalLink);

            GetByIdExternalLinkResponse response = _mapper.Map<GetByIdExternalLinkResponse>(externalLink);
            return response;
        }
    }
}
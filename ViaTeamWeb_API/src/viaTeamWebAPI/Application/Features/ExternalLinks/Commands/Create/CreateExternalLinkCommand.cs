using Application.Features.ExternalLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ExternalLinks.Commands.Create;

public class CreateExternalLinkCommand : IRequest<CreatedExternalLinkResponse>
{
    public string Name { get; set; }
    public string Url { get; set; }
    public Guid? MemberId { get; set; }
    public Guid? TeamId { get; set; }

    public class CreateExternalLinkCommandHandler : IRequestHandler<CreateExternalLinkCommand, CreatedExternalLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly IExternalLinkRepository _externalLinkRepository;
        private readonly ExternalLinkBusinessRules _externalLinkBusinessRules;

        public CreateExternalLinkCommandHandler(IMapper mapper, IExternalLinkRepository externalLinkRepository,
                                         ExternalLinkBusinessRules externalLinkBusinessRules)
        {
            _mapper = mapper;
            _externalLinkRepository = externalLinkRepository;
            _externalLinkBusinessRules = externalLinkBusinessRules;
        }

        public async Task<CreatedExternalLinkResponse> Handle(CreateExternalLinkCommand request, CancellationToken cancellationToken)
        {
            ExternalLink externalLink = _mapper.Map<ExternalLink>(request);

            await _externalLinkRepository.AddAsync(externalLink);

            CreatedExternalLinkResponse response = _mapper.Map<CreatedExternalLinkResponse>(externalLink);
            return response;
        }
    }
}
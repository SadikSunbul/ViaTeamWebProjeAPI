using Application.Features.ExternalLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ExternalLinks.Commands.Update;

public class UpdateExternalLinkCommand : IRequest<UpdatedExternalLinkResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public Guid? MemberId { get; set; }
    public Guid? TeamId { get; set; }
  

    public class UpdateExternalLinkCommandHandler : IRequestHandler<UpdateExternalLinkCommand, UpdatedExternalLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly IExternalLinkRepository _externalLinkRepository;
        private readonly ExternalLinkBusinessRules _externalLinkBusinessRules;

        public UpdateExternalLinkCommandHandler(IMapper mapper, IExternalLinkRepository externalLinkRepository,
                                         ExternalLinkBusinessRules externalLinkBusinessRules)
        {
            _mapper = mapper;
            _externalLinkRepository = externalLinkRepository;
            _externalLinkBusinessRules = externalLinkBusinessRules;
        }

        public async Task<UpdatedExternalLinkResponse> Handle(UpdateExternalLinkCommand request, CancellationToken cancellationToken)
        {
            ExternalLink? externalLink = await _externalLinkRepository.GetAsync(predicate: el => el.Id == request.Id, cancellationToken: cancellationToken);
            await _externalLinkBusinessRules.ExternalLinkShouldExistWhenSelected(externalLink);
            externalLink = _mapper.Map(request, externalLink);

            await _externalLinkRepository.UpdateAsync(externalLink!);

            UpdatedExternalLinkResponse response = _mapper.Map<UpdatedExternalLinkResponse>(externalLink);
            return response;
        }
    }
}
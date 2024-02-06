using Application.Features.ExternalLinks.Constants;
using Application.Features.ExternalLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ExternalLinks.Commands.Delete;

public class DeleteExternalLinkCommand : IRequest<DeletedExternalLinkResponse>
{
    public Guid Id { get; set; }
    public int UserId { get; set; }

    public class
        DeleteExternalLinkCommandHandler : IRequestHandler<DeleteExternalLinkCommand,
            DeletedExternalLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly IExternalLinkRepository _externalLinkRepository;
        private readonly ExternalLinkBusinessRules _externalLinkBusinessRules;

        public DeleteExternalLinkCommandHandler(IMapper mapper,
            IExternalLinkRepository externalLinkRepository,
            ExternalLinkBusinessRules externalLinkBusinessRules, IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _externalLinkRepository = externalLinkRepository;
            _externalLinkBusinessRules = externalLinkBusinessRules;
            _memberRepository = memberRepository;
        }

        public async Task<DeletedExternalLinkResponse> Handle(DeleteExternalLinkCommand request,
            CancellationToken cancellationToken)
        {
            Guid MemberId = (await _memberRepository.GetAsync(i => i.UserId == request.UserId)).Id;
            ExternalLink? externalLink =
                await _externalLinkRepository.GetAsync(
                    predicate: el => el.Id == request.Id && el.MemberId == MemberId,
                    cancellationToken: cancellationToken);
            await _externalLinkBusinessRules.ExternalLinkShouldExistWhenSelected(externalLink);

            await _externalLinkRepository.DeleteAsync(externalLink!, true);

            DeletedExternalLinkResponse response = _mapper.Map<DeletedExternalLinkResponse>(externalLink);
            return response;
        }
    }
}
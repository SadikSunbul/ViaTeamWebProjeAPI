using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.ExternalLinks.Queries.GetList;

public class GetListExternalLinkQuery : IRequest<GetListResponse<GetListExternalLinkListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListExternalLinkQueryHandler : IRequestHandler<GetListExternalLinkQuery, GetListResponse<GetListExternalLinkListItemDto>>
    {
        private readonly IExternalLinkRepository _externalLinkRepository;
        private readonly IMapper _mapper;

        public GetListExternalLinkQueryHandler(IExternalLinkRepository externalLinkRepository, IMapper mapper)
        {
            _externalLinkRepository = externalLinkRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListExternalLinkListItemDto>> Handle(GetListExternalLinkQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ExternalLink> externalLinks = await _externalLinkRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListExternalLinkListItemDto> response = _mapper.Map<GetListResponse<GetListExternalLinkListItemDto>>(externalLinks);
            return response;
        }
    }
}
using Application.Features.HeroSectionWrites.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.HeroSectionWrites.Constants.HeroSectionWritesOperationClaims;

namespace Application.Features.HeroSectionWrites.Queries.GetList;

public class GetListHeroSectionWriteQuery : IRequest<GetListResponse<GetListHeroSectionWriteListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListHeroSectionWriteQueryHandler : IRequestHandler<GetListHeroSectionWriteQuery, GetListResponse<GetListHeroSectionWriteListItemDto>>
    {
        private readonly IHeroSectionWriteRepository _heroSectionWriteRepository;
        private readonly IMapper _mapper;

        public GetListHeroSectionWriteQueryHandler(IHeroSectionWriteRepository heroSectionWriteRepository, IMapper mapper)
        {
            _heroSectionWriteRepository = heroSectionWriteRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListHeroSectionWriteListItemDto>> Handle(GetListHeroSectionWriteQuery request, CancellationToken cancellationToken)
        {
            IPaginate<HeroSectionWrite> heroSectionWrites = await _heroSectionWriteRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListHeroSectionWriteListItemDto> response = _mapper.Map<GetListResponse<GetListHeroSectionWriteListItemDto>>(heroSectionWrites);
            return response;
        }
    }
}
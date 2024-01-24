using Application.Features.Denemes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Denemes.Constants.DenemesOperationClaims;

namespace Application.Features.Denemes.Queries.GetList;

public class GetListDenemeQuery : IRequest<GetListResponse<GetListDenemeListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListDenemeQueryHandler : IRequestHandler<GetListDenemeQuery, GetListResponse<GetListDenemeListItemDto>>
    {
        private readonly IDenemeRepository _denemeRepository;
        private readonly IMapper _mapper;

        public GetListDenemeQueryHandler(IDenemeRepository denemeRepository, IMapper mapper)
        {
            _denemeRepository = denemeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDenemeListItemDto>> Handle(GetListDenemeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Deneme> denemes = await _denemeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListDenemeListItemDto> response = _mapper.Map<GetListResponse<GetListDenemeListItemDto>>(denemes);
            return response;
        }
    }
}
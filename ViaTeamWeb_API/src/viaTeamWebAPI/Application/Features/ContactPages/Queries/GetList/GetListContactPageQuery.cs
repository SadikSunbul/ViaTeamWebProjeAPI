using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.ContactPages.Queries.GetList;

public class GetListContactPageQuery : IRequest<GetListResponse<GetListContactPageListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListContactPageQueryHandler : IRequestHandler<GetListContactPageQuery, GetListResponse<GetListContactPageListItemDto>>
    {
        private readonly IContactPageRepository _contactPageRepository;
        private readonly IMapper _mapper;

        public GetListContactPageQueryHandler(IContactPageRepository contactPageRepository, IMapper mapper)
        {
            _contactPageRepository = contactPageRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContactPageListItemDto>> Handle(GetListContactPageQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContactPage> contactPages = await _contactPageRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContactPageListItemDto> response = _mapper.Map<GetListResponse<GetListContactPageListItemDto>>(contactPages);
            return response;
        }
    }
}
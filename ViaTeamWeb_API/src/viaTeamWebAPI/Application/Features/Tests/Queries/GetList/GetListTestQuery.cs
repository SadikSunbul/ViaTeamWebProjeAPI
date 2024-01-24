using Application.Features.Tests.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Tests.Constants.TestsOperationClaims;

namespace Application.Features.Tests.Queries.GetList;

public class GetListTestQuery : IRequest<GetListResponse<GetListTestListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetListTestQueryHandler : IRequestHandler<GetListTestQuery, GetListResponse<GetListTestListItemDto>>
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public GetListTestQueryHandler(ITestRepository testRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTestListItemDto>> Handle(GetListTestQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Test> tests = await _testRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTestListItemDto> response = _mapper.Map<GetListResponse<GetListTestListItemDto>>(tests);
            return response;
        }
    }
}
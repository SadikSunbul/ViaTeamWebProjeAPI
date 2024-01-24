using Application.Features.Tests.Constants;
using Application.Features.Tests.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Tests.Constants.TestsOperationClaims;

namespace Application.Features.Tests.Queries.GetById;

public class GetByIdTestQuery : IRequest<GetByIdTestResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdTestQueryHandler : IRequestHandler<GetByIdTestQuery, GetByIdTestResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;
        private readonly TestBusinessRules _testBusinessRules;

        public GetByIdTestQueryHandler(IMapper mapper, ITestRepository testRepository, TestBusinessRules testBusinessRules)
        {
            _mapper = mapper;
            _testRepository = testRepository;
            _testBusinessRules = testBusinessRules;
        }

        public async Task<GetByIdTestResponse> Handle(GetByIdTestQuery request, CancellationToken cancellationToken)
        {
            Test? test = await _testRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _testBusinessRules.TestShouldExistWhenSelected(test);

            GetByIdTestResponse response = _mapper.Map<GetByIdTestResponse>(test);
            return response;
        }
    }
}
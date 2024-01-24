using Application.Features.Tests.Constants;
using Application.Features.Tests.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Tests.Constants.TestsOperationClaims;

namespace Application.Features.Tests.Commands.Create;

public class CreateTestCommand : IRequest<CreatedTestResponse>, ISecuredRequest
{
    public string NAME { get; set; }

    public string[] Roles => new[] { Admin, StandardUser, Write, TestsOperationClaims.Create };

    public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, CreatedTestResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;
        private readonly TestBusinessRules _testBusinessRules;

        public CreateTestCommandHandler(IMapper mapper, ITestRepository testRepository,
            TestBusinessRules testBusinessRules)
        {
            _mapper = mapper;
            _testRepository = testRepository;
            _testBusinessRules = testBusinessRules;
        }

        public async Task<CreatedTestResponse> Handle(CreateTestCommand request,
            CancellationToken cancellationToken)
        {
            Test test = _mapper.Map<Test>(request);

            await _testRepository.AddAsync(test);

            CreatedTestResponse response = _mapper.Map<CreatedTestResponse>(test);
            return response;
        }
    }
}
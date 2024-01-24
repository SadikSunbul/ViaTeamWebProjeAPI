using Application.Features.Tests.Constants;
using Application.Features.Tests.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Tests.Constants.TestsOperationClaims;

namespace Application.Features.Tests.Commands.Update;

public class UpdateTestCommand : IRequest<UpdatedTestResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string NAME { get; set; }

    public string[] Roles => new[] { Admin, Write, TestsOperationClaims.Update };

    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, UpdatedTestResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;
        private readonly TestBusinessRules _testBusinessRules;

        public UpdateTestCommandHandler(IMapper mapper, ITestRepository testRepository,
                                         TestBusinessRules testBusinessRules)
        {
            _mapper = mapper;
            _testRepository = testRepository;
            _testBusinessRules = testBusinessRules;
        }

        public async Task<UpdatedTestResponse> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            Test? test = await _testRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _testBusinessRules.TestShouldExistWhenSelected(test);
            test = _mapper.Map(request, test);

            await _testRepository.UpdateAsync(test!);

            UpdatedTestResponse response = _mapper.Map<UpdatedTestResponse>(test);
            return response;
        }
    }
}
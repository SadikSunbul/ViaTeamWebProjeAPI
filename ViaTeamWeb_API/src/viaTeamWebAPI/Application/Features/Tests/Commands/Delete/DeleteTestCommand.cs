using Application.Features.Tests.Constants;
using Application.Features.Tests.Constants;
using Application.Features.Tests.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Tests.Constants.TestsOperationClaims;

namespace Application.Features.Tests.Commands.Delete;

public class DeleteTestCommand : IRequest<DeletedTestResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, TestsOperationClaims.Delete };

    public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand, DeletedTestResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;
        private readonly TestBusinessRules _testBusinessRules;

        public DeleteTestCommandHandler(IMapper mapper, ITestRepository testRepository,
                                         TestBusinessRules testBusinessRules)
        {
            _mapper = mapper;
            _testRepository = testRepository;
            _testBusinessRules = testBusinessRules;
        }

        public async Task<DeletedTestResponse> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
        {
            Test? test = await _testRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _testBusinessRules.TestShouldExistWhenSelected(test);

            await _testRepository.DeleteAsync(test!);

            DeletedTestResponse response = _mapper.Map<DeletedTestResponse>(test);
            return response;
        }
    }
}
using Application.Features.Denemes.Constants;
using Application.Features.Denemes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using MediatR;
using static Application.Features.Denemes.Constants.DenemesOperationClaims;

namespace Application.Features.Denemes.Commands.Create;

public class CreateDenemeCommand : IRequest<CreatedDenemeResponse>, ISecuredRequest, ILoggableRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, DenemesOperationClaims.Create };

    public class CreateDenemeCommandHandler : IRequestHandler<CreateDenemeCommand, CreatedDenemeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDenemeRepository _denemeRepository;
        private readonly DenemeBusinessRules _denemeBusinessRules;

        public CreateDenemeCommandHandler(IMapper mapper, IDenemeRepository denemeRepository,
                                         DenemeBusinessRules denemeBusinessRules)
        {
            _mapper = mapper;
            _denemeRepository = denemeRepository;
            _denemeBusinessRules = denemeBusinessRules;
        }

        public async Task<CreatedDenemeResponse> Handle(CreateDenemeCommand request, CancellationToken cancellationToken)
        {
            Deneme deneme = _mapper.Map<Deneme>(request);

            await _denemeRepository.AddAsync(deneme);

            CreatedDenemeResponse response = _mapper.Map<CreatedDenemeResponse>(deneme);
            return response;
        }
    }
}
using Application.Features.Alooos.Constants;
using Application.Features.Alooos.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Alooos.Constants.AlooosOperationClaims;

namespace Application.Features.Alooos.Commands.Create;

public class CreateAloooCommand : IRequest<CreatedAloooResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Deneme { get; set; }

    public string[] Roles => new[] { Admin, Write, AlooosOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAlooos";

    public class CreateAloooCommandHandler : IRequestHandler<CreateAloooCommand, CreatedAloooResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAloooRepository _aloooRepository;
        private readonly AloooBusinessRules _aloooBusinessRules;

        public CreateAloooCommandHandler(IMapper mapper, IAloooRepository aloooRepository,
                                         AloooBusinessRules aloooBusinessRules)
        {
            _mapper = mapper;
            _aloooRepository = aloooRepository;
            _aloooBusinessRules = aloooBusinessRules;
        }

        public async Task<CreatedAloooResponse> Handle(CreateAloooCommand request, CancellationToken cancellationToken)
        {
            Alooo alooo = _mapper.Map<Alooo>(request);

            await _aloooRepository.AddAsync(alooo);

            CreatedAloooResponse response = _mapper.Map<CreatedAloooResponse>(alooo);
            return response;
        }
    }
}
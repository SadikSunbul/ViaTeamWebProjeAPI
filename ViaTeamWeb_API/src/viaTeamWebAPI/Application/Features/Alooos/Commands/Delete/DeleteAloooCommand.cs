using Application.Features.Alooos.Constants;
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

namespace Application.Features.Alooos.Commands.Delete;

public class DeleteAloooCommand : IRequest<DeletedAloooResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, AlooosOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAlooos";

    public class DeleteAloooCommandHandler : IRequestHandler<DeleteAloooCommand, DeletedAloooResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAloooRepository _aloooRepository;
        private readonly AloooBusinessRules _aloooBusinessRules;

        public DeleteAloooCommandHandler(IMapper mapper, IAloooRepository aloooRepository,
                                         AloooBusinessRules aloooBusinessRules)
        {
            _mapper = mapper;
            _aloooRepository = aloooRepository;
            _aloooBusinessRules = aloooBusinessRules;
        }

        public async Task<DeletedAloooResponse> Handle(DeleteAloooCommand request, CancellationToken cancellationToken)
        {
            Alooo? alooo = await _aloooRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _aloooBusinessRules.AloooShouldExistWhenSelected(alooo);

            await _aloooRepository.DeleteAsync(alooo!);

            DeletedAloooResponse response = _mapper.Map<DeletedAloooResponse>(alooo);
            return response;
        }
    }
}
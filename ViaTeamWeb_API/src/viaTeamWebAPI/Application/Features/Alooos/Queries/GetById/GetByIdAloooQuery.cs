using Application.Features.Alooos.Constants;
using Application.Features.Alooos.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Alooos.Constants.AlooosOperationClaims;

namespace Application.Features.Alooos.Queries.GetById;

public class GetByIdAloooQuery : IRequest<GetByIdAloooResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdAloooQueryHandler : IRequestHandler<GetByIdAloooQuery, GetByIdAloooResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAloooRepository _aloooRepository;
        private readonly AloooBusinessRules _aloooBusinessRules;

        public GetByIdAloooQueryHandler(IMapper mapper, IAloooRepository aloooRepository, AloooBusinessRules aloooBusinessRules)
        {
            _mapper = mapper;
            _aloooRepository = aloooRepository;
            _aloooBusinessRules = aloooBusinessRules;
        }

        public async Task<GetByIdAloooResponse> Handle(GetByIdAloooQuery request, CancellationToken cancellationToken)
        {
            Alooo? alooo = await _aloooRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _aloooBusinessRules.AloooShouldExistWhenSelected(alooo);

            GetByIdAloooResponse response = _mapper.Map<GetByIdAloooResponse>(alooo);
            return response;
        }
    }
}
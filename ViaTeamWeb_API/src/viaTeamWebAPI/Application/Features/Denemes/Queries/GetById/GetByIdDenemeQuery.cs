using Application.Features.Denemes.Constants;
using Application.Features.Denemes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Denemes.Constants.DenemesOperationClaims;

namespace Application.Features.Denemes.Queries.GetById;

public class GetByIdDenemeQuery : IRequest<GetByIdDenemeResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdDenemeQueryHandler : IRequestHandler<GetByIdDenemeQuery, GetByIdDenemeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDenemeRepository _denemeRepository;
        private readonly DenemeBusinessRules _denemeBusinessRules;

        public GetByIdDenemeQueryHandler(IMapper mapper, IDenemeRepository denemeRepository, DenemeBusinessRules denemeBusinessRules)
        {
            _mapper = mapper;
            _denemeRepository = denemeRepository;
            _denemeBusinessRules = denemeBusinessRules;
        }

        public async Task<GetByIdDenemeResponse> Handle(GetByIdDenemeQuery request, CancellationToken cancellationToken)
        {
            Deneme? deneme = await _denemeRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await _denemeBusinessRules.DenemeShouldExistWhenSelected(deneme);

            GetByIdDenemeResponse response = _mapper.Map<GetByIdDenemeResponse>(deneme);
            return response;
        }
    }
}
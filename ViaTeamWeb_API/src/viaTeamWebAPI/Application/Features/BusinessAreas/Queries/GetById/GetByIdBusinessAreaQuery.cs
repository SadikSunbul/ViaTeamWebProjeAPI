using Application.Features.BusinessAreas.Constants;
using Application.Features.BusinessAreas.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.BusinessAreas.Constants.BusinessAreasOperationClaims;

namespace Application.Features.BusinessAreas.Queries.GetById;

public class GetByIdBusinessAreaQuery : IRequest<GetByIdBusinessAreaResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdBusinessAreaQueryHandler : IRequestHandler<GetByIdBusinessAreaQuery, GetByIdBusinessAreaResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessAreaRepository _businessAreaRepository;
        private readonly BusinessAreaBusinessRules _businessAreaBusinessRules;

        public GetByIdBusinessAreaQueryHandler(IMapper mapper, IBusinessAreaRepository businessAreaRepository, BusinessAreaBusinessRules businessAreaBusinessRules)
        {
            _mapper = mapper;
            _businessAreaRepository = businessAreaRepository;
            _businessAreaBusinessRules = businessAreaBusinessRules;
        }

        public async Task<GetByIdBusinessAreaResponse> Handle(GetByIdBusinessAreaQuery request, CancellationToken cancellationToken)
        {
            BusinessArea? businessArea = await _businessAreaRepository.GetAsync(predicate: ba => ba.Id == request.Id, cancellationToken: cancellationToken);
            await _businessAreaBusinessRules.BusinessAreaShouldExistWhenSelected(businessArea);

            GetByIdBusinessAreaResponse response = _mapper.Map<GetByIdBusinessAreaResponse>(businessArea);
            return response;
        }
    }
}
using Application.Features.BusinessAreas.Constants;
using Application.Features.BusinessAreas.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.BusinessAreas.Constants.BusinessAreasOperationClaims;

namespace Application.Features.BusinessAreas.Commands.Create;

public class CreateBusinessAreaCommand : IRequest<CreatedBusinessAreaResponse>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, BusinessAreasOperationClaims.Create };

    public class CreateBusinessAreaCommandHandler : IRequestHandler<CreateBusinessAreaCommand, CreatedBusinessAreaResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessAreaRepository _businessAreaRepository;
        private readonly BusinessAreaBusinessRules _businessAreaBusinessRules;

        public CreateBusinessAreaCommandHandler(IMapper mapper, IBusinessAreaRepository businessAreaRepository,
                                         BusinessAreaBusinessRules businessAreaBusinessRules)
        {
            _mapper = mapper;
            _businessAreaRepository = businessAreaRepository;
            _businessAreaBusinessRules = businessAreaBusinessRules;
        }

        public async Task<CreatedBusinessAreaResponse> Handle(CreateBusinessAreaCommand request, CancellationToken cancellationToken)
        {
            BusinessArea businessArea = _mapper.Map<BusinessArea>(request);

            await _businessAreaRepository.AddAsync(businessArea);

            CreatedBusinessAreaResponse response = _mapper.Map<CreatedBusinessAreaResponse>(businessArea);
            return response;
        }
    }
}
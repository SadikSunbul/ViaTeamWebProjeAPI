using Application.Features.BusinessAreas.Constants;
using Application.Features.BusinessAreas.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.BusinessAreas.Constants.BusinessAreasOperationClaims;

namespace Application.Features.BusinessAreas.Commands.Update;

public class UpdateBusinessAreaCommand : IRequest<UpdatedBusinessAreaResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, BusinessAreasOperationClaims.Update };

    public class UpdateBusinessAreaCommandHandler : IRequestHandler<UpdateBusinessAreaCommand, UpdatedBusinessAreaResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessAreaRepository _businessAreaRepository;
        private readonly BusinessAreaBusinessRules _businessAreaBusinessRules;

        public UpdateBusinessAreaCommandHandler(IMapper mapper, IBusinessAreaRepository businessAreaRepository,
                                         BusinessAreaBusinessRules businessAreaBusinessRules)
        {
            _mapper = mapper;
            _businessAreaRepository = businessAreaRepository;
            _businessAreaBusinessRules = businessAreaBusinessRules;
        }

        public async Task<UpdatedBusinessAreaResponse> Handle(UpdateBusinessAreaCommand request, CancellationToken cancellationToken)
        {
            BusinessArea? businessArea = await _businessAreaRepository.GetAsync(predicate: ba => ba.Id == request.Id, cancellationToken: cancellationToken);
            await _businessAreaBusinessRules.BusinessAreaShouldExistWhenSelected(businessArea);
            businessArea = _mapper.Map(request, businessArea);

            await _businessAreaRepository.UpdateAsync(businessArea!);

            UpdatedBusinessAreaResponse response = _mapper.Map<UpdatedBusinessAreaResponse>(businessArea);
            return response;
        }
    }
}
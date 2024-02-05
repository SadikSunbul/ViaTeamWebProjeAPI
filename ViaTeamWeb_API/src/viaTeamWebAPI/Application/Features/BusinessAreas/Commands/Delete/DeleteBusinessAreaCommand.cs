using Application.Features.BusinessAreas.Constants;
using Application.Features.BusinessAreas.Constants;
using Application.Features.BusinessAreas.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.BusinessAreas.Constants.BusinessAreasOperationClaims;

namespace Application.Features.BusinessAreas.Commands.Delete;

public class DeleteBusinessAreaCommand : IRequest<DeletedBusinessAreaResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, BusinessAreasOperationClaims.Delete };

    public class DeleteBusinessAreaCommandHandler : IRequestHandler<DeleteBusinessAreaCommand, DeletedBusinessAreaResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessAreaRepository _businessAreaRepository;
        private readonly BusinessAreaBusinessRules _businessAreaBusinessRules;

        public DeleteBusinessAreaCommandHandler(IMapper mapper, IBusinessAreaRepository businessAreaRepository,
                                         BusinessAreaBusinessRules businessAreaBusinessRules)
        {
            _mapper = mapper;
            _businessAreaRepository = businessAreaRepository;
            _businessAreaBusinessRules = businessAreaBusinessRules;
        }

        public async Task<DeletedBusinessAreaResponse> Handle(DeleteBusinessAreaCommand request, CancellationToken cancellationToken)
        {
            BusinessArea? businessArea = await _businessAreaRepository.GetAsync(predicate: ba => ba.Id == request.Id, cancellationToken: cancellationToken);
            await _businessAreaBusinessRules.BusinessAreaShouldExistWhenSelected(businessArea);

            await _businessAreaRepository.DeleteAsync(businessArea!);

            DeletedBusinessAreaResponse response = _mapper.Map<DeletedBusinessAreaResponse>(businessArea);
            return response;
        }
    }
}
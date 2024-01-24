using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using static Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimResponse>, ISecuredRequest
{
    public string Name { get; set; }

    public CreateOperationClaimCommand()
    {
        Name = string.Empty;
    }

    public CreateOperationClaimCommand(string name)
    {
        Name = name;
    }

    public string[] Roles => new[] { Admin, Write, Add };

    public class
        CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand,
            CreatedOperationClaimResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public CreateOperationClaimCommandHandler(
            IOperationClaimRepository operationClaimRepository,
            IMapper mapper,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<CreatedOperationClaimResponse> Handle(CreateOperationClaimCommand request,
            CancellationToken cancellationToken)
        {
            // İş kuralı: Eklenmek istenen işlem yetkisi adının daha önce kullanılmamış olması gerekiyor.
            await _operationClaimBusinessRules.OperationClaimNameShouldNotExistWhenCreating(request.Name);
            // CreateOperationClaimCommand nesnesini, OperationClaim tipine dönüştürerek bir nesne oluşturulur.
            OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
            // Oluşturulan işlem yetkisi veritabanına eklenir.
            OperationClaim createdOperationClaim =
                await _operationClaimRepository.AddAsync(mappedOperationClaim);
            // Oluşturulan işlem yetkisi bilgileri, CreatedOperationClaimResponse tipine dönüştürülerek bir yanıt nesnesi oluşturulur.
            CreatedOperationClaimResponse response =
                _mapper.Map<CreatedOperationClaimResponse>(createdOperationClaim);
            return response;
        }
    }
}
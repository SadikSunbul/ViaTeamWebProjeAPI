using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using static Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommand : IRequest<CreatedUserOperationClaimResponse>, ISecuredRequest
{
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateUserOperationClaimCommandHandler
        : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimResponse>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public CreateUserOperationClaimCommandHandler(
            IUserOperationClaimRepository userOperationClaimRepository,
            IMapper mapper,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules
        )
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<CreatedUserOperationClaimResponse> Handle(
            CreateUserOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            // Kullanıcının belirli bir işlem yetkisine sahip olup olmadığını kontrol eden iş kurallarını kontrol et.
            await _userOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenInsert(
                request.UserId,
                request.OperationClaimId
            );
            // Kullanıcının veritabanına eklenmek üzere işlem yetkisi bilgilerini içeren bir UserOperationClaim nesnesi oluşturulur.
            UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);

            // UserOperationClaim nesnesi, veritabanına eklenir.
            UserOperationClaim createdUserOperationClaim =
                await _userOperationClaimRepository.AddAsync(mappedUserOperationClaim);

            // Oluşturulan UserOperationClaim nesnesi, CreatedUserOperationClaimResponse tipine dönüştürülür.
            CreatedUserOperationClaimResponse createdUserOperationClaimDto =
                _mapper.Map<CreatedUserOperationClaimResponse>(
                    createdUserOperationClaim
                );
            return createdUserOperationClaimDto;
        }
    }
}
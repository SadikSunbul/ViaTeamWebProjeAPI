using Application.Features.UserOperationClaims.Constants;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using static Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommand : IRequest<UpdatedUserOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public string[] Roles => new[] { Admin, Write, UserOperationClaimsOperationClaims.Update };

    public class UpdateUserOperationClaimCommandHandler
        : IRequestHandler<UpdateUserOperationClaimCommand, UpdatedUserOperationClaimResponse>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public UpdateUserOperationClaimCommandHandler(
            IUserOperationClaimRepository userOperationClaimRepository,
            IMapper mapper,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules
        )
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<UpdatedUserOperationClaimResponse> Handle(
            UpdateUserOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            // İlgili Id'ye sahip UserOperationClaim nesnesini veritabanından getir.
            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(
                predicate: uoc => uoc.Id == request.Id,
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            // Kullanıcı işlem yetkisi bulunamazsa hata fırlat.
            await _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenSelected(
                userOperationClaim);
            // Güncelleme işlemi öncesinde, kullanıcının işlem yetkisini güncelleyip güncelleyemeyeceğini kontrol et.
            await _userOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenUpdated(
                request.Id,
                request.UserId,
                request.OperationClaimId
            );
            // İlgili UserOperationClaim nesnesini, API tarafından gelen güncelleme isteği verileri ile haritala.
            UserOperationClaim mappedUserOperationClaim =
                _mapper.Map(request, destination: userOperationClaim!);
            // Haritalanan UserOperationClaim nesnesini veritabanında güncelle.
            UserOperationClaim updatedUserOperationClaim =
                await _userOperationClaimRepository.UpdateAsync(mappedUserOperationClaim);

            // Güncellenen UserOperationClaim nesnesini, API tarafına uygun DTO'ya dönüştür.
            UpdatedUserOperationClaimResponse updatedUserOperationClaimDto =
                _mapper.Map<UpdatedUserOperationClaimResponse>(
                    updatedUserOperationClaim
                );
            return updatedUserOperationClaimDto;
        }
    }
}
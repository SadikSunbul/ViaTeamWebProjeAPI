using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserQuery : IRequest<GetByIdUserResponse>
{
    public int Id { get; set; }

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper,
            UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request,
            CancellationToken cancellationToken)
        {
            // UserRepository kullanarak, GetByIdUserQuery ile gelen ID'ye sahip kullanıcıyı veritabanından getir.
            User? user = await _userRepository.GetAsync(predicate: b => b.Id == request.Id,
                cancellationToken: cancellationToken);

            // Eğer kullanıcı bulunamazsa veya null ise, iş kuralı hatası fırlatılır.
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            // AutoMapper kullanarak, veritabanından alınan User nesnesini GetByIdUserResponse tipine dönüştür.
            GetByIdUserResponse response = _mapper.Map<GetByIdUserResponse>(user);
            return response;
        }
    }
}
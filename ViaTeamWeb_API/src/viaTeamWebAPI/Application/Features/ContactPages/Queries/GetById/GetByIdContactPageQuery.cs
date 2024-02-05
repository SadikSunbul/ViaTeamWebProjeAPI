using Application.Features.ContactPages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ContactPages.Queries.GetById;

public class GetByIdContactPageQuery : IRequest<GetByIdContactPageResponse>
{
    public Guid Id { get; set; }

    public class GetByIdContactPageQueryHandler : IRequestHandler<GetByIdContactPageQuery, GetByIdContactPageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContactPageRepository _contactPageRepository;
        private readonly ContactPageBusinessRules _contactPageBusinessRules;

        public GetByIdContactPageQueryHandler(IMapper mapper, IContactPageRepository contactPageRepository, ContactPageBusinessRules contactPageBusinessRules)
        {
            _mapper = mapper;
            _contactPageRepository = contactPageRepository;
            _contactPageBusinessRules = contactPageBusinessRules;
        }

        public async Task<GetByIdContactPageResponse> Handle(GetByIdContactPageQuery request, CancellationToken cancellationToken)
        {
            ContactPage? contactPage = await _contactPageRepository.GetAsync(predicate: cp => cp.Id == request.Id, cancellationToken: cancellationToken);
            await _contactPageBusinessRules.ContactPageShouldExistWhenSelected(contactPage);

            GetByIdContactPageResponse response = _mapper.Map<GetByIdContactPageResponse>(contactPage);
            return response;
        }
    }
}
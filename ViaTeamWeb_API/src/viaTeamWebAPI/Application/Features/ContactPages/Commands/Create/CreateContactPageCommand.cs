using Application.Features.ContactPages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.ContactPages.Commands.Create;

public class CreateContactPageCommand : IRequest<CreatedContactPageResponse>, ITransactionalRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }

    public class CreateContactPageCommandHandler : IRequestHandler<CreateContactPageCommand, CreatedContactPageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContactPageRepository _contactPageRepository;
        private readonly ContactPageBusinessRules _contactPageBusinessRules;

        public CreateContactPageCommandHandler(IMapper mapper, IContactPageRepository contactPageRepository,
                                         ContactPageBusinessRules contactPageBusinessRules)
        {
            _mapper = mapper;
            _contactPageRepository = contactPageRepository;
            _contactPageBusinessRules = contactPageBusinessRules;
        }

        public async Task<CreatedContactPageResponse> Handle(CreateContactPageCommand request, CancellationToken cancellationToken)
        {
            ContactPage contactPage = _mapper.Map<ContactPage>(request);

            await _contactPageRepository.AddAsync(contactPage);

            CreatedContactPageResponse response = _mapper.Map<CreatedContactPageResponse>(contactPage);
            return response;
        }
    }
}
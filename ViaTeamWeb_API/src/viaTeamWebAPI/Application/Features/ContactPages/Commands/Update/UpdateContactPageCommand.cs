using Application.Features.ContactPages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.ContactPages.Commands.Update;

public class UpdateContactPageCommand : IRequest<UpdatedContactPageResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }

    public class UpdateContactPageCommandHandler : IRequestHandler<UpdateContactPageCommand, UpdatedContactPageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContactPageRepository _contactPageRepository;
        private readonly ContactPageBusinessRules _contactPageBusinessRules;

        public UpdateContactPageCommandHandler(IMapper mapper, IContactPageRepository contactPageRepository,
                                         ContactPageBusinessRules contactPageBusinessRules)
        {
            _mapper = mapper;
            _contactPageRepository = contactPageRepository;
            _contactPageBusinessRules = contactPageBusinessRules;
        }

        public async Task<UpdatedContactPageResponse> Handle(UpdateContactPageCommand request, CancellationToken cancellationToken)
        {
            ContactPage? contactPage = await _contactPageRepository.GetAsync(predicate: cp => cp.Id == request.Id, cancellationToken: cancellationToken);
            await _contactPageBusinessRules.ContactPageShouldExistWhenSelected(contactPage);
            contactPage = _mapper.Map(request, contactPage);

            await _contactPageRepository.UpdateAsync(contactPage!);

            UpdatedContactPageResponse response = _mapper.Map<UpdatedContactPageResponse>(contactPage);
            return response;
        }
    }
}
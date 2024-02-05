using Application.Features.ContactPages.Constants;
using Application.Features.ContactPages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.ContactPages.Commands.Delete;

public class DeleteContactPageCommand : IRequest<DeletedContactPageResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteContactPageCommandHandler : IRequestHandler<DeleteContactPageCommand, DeletedContactPageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContactPageRepository _contactPageRepository;
        private readonly ContactPageBusinessRules _contactPageBusinessRules;

        public DeleteContactPageCommandHandler(IMapper mapper, IContactPageRepository contactPageRepository,
                                         ContactPageBusinessRules contactPageBusinessRules)
        {
            _mapper = mapper;
            _contactPageRepository = contactPageRepository;
            _contactPageBusinessRules = contactPageBusinessRules;
        }

        public async Task<DeletedContactPageResponse> Handle(DeleteContactPageCommand request, CancellationToken cancellationToken)
        {
            ContactPage? contactPage = await _contactPageRepository.GetAsync(predicate: cp => cp.Id == request.Id, cancellationToken: cancellationToken);
            await _contactPageBusinessRules.ContactPageShouldExistWhenSelected(contactPage);

            await _contactPageRepository.DeleteAsync(contactPage!);

            DeletedContactPageResponse response = _mapper.Map<DeletedContactPageResponse>(contactPage);
            return response;
        }
    }
}
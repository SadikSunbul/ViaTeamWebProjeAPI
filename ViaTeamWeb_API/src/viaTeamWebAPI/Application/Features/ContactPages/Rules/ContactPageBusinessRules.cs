using Application.Features.ContactPages.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContactPages.Rules;

public class ContactPageBusinessRules : BaseBusinessRules
{
    private readonly IContactPageRepository _contactPageRepository;

    public ContactPageBusinessRules(IContactPageRepository contactPageRepository)
    {
        _contactPageRepository = contactPageRepository;
    }

    public Task ContactPageShouldExistWhenSelected(ContactPage? contactPage)
    {
        if (contactPage == null)
            throw new BusinessException(ContactPagesBusinessMessages.ContactPageNotExists);
        return Task.CompletedTask;
    }

    public async Task ContactPageIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ContactPage? contactPage = await _contactPageRepository.GetAsync(
            predicate: cp => cp.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContactPageShouldExistWhenSelected(contactPage);
    }
}
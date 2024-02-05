using Application.Features.ContactPages.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContactPages;

public class ContactPagesManager : IContactPagesService
{
    private readonly IContactPageRepository _contactPageRepository;
    private readonly ContactPageBusinessRules _contactPageBusinessRules;

    public ContactPagesManager(IContactPageRepository contactPageRepository, ContactPageBusinessRules contactPageBusinessRules)
    {
        _contactPageRepository = contactPageRepository;
        _contactPageBusinessRules = contactPageBusinessRules;
    }

    public async Task<ContactPage?> GetAsync(
        Expression<Func<ContactPage, bool>> predicate,
        Func<IQueryable<ContactPage>, IIncludableQueryable<ContactPage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContactPage? contactPage = await _contactPageRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contactPage;
    }

    public async Task<IPaginate<ContactPage>?> GetListAsync(
        Expression<Func<ContactPage, bool>>? predicate = null,
        Func<IQueryable<ContactPage>, IOrderedQueryable<ContactPage>>? orderBy = null,
        Func<IQueryable<ContactPage>, IIncludableQueryable<ContactPage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContactPage> contactPageList = await _contactPageRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contactPageList;
    }

    public async Task<ContactPage> AddAsync(ContactPage contactPage)
    {
        ContactPage addedContactPage = await _contactPageRepository.AddAsync(contactPage);

        return addedContactPage;
    }

    public async Task<ContactPage> UpdateAsync(ContactPage contactPage)
    {
        ContactPage updatedContactPage = await _contactPageRepository.UpdateAsync(contactPage);

        return updatedContactPage;
    }

    public async Task<ContactPage> DeleteAsync(ContactPage contactPage, bool permanent = false)
    {
        ContactPage deletedContactPage = await _contactPageRepository.DeleteAsync(contactPage);

        return deletedContactPage;
    }
}

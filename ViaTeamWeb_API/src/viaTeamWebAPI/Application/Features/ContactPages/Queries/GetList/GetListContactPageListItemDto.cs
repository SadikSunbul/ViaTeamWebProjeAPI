using Core.Application.Dtos;

namespace Application.Features.ContactPages.Queries.GetList;

public class GetListContactPageListItemDto : IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
}
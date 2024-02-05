using Core.Application.Responses;

namespace Application.Features.ContactPages.Queries.GetById;

public class GetByIdContactPageResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
}
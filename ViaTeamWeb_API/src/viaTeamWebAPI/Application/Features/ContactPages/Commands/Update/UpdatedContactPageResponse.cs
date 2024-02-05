using Core.Application.Responses;

namespace Application.Features.ContactPages.Commands.Update;

public class UpdatedContactPageResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
}
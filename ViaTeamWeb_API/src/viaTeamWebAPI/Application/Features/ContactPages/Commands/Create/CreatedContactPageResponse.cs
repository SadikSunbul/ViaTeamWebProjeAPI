using Core.Application.Responses;

namespace Application.Features.ContactPages.Commands.Create;

public class CreatedContactPageResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
}
using FluentValidation;

namespace Application.Features.ContactPages.Commands.Create;

public class CreateContactPageCommandValidator : AbstractValidator<CreateContactPageCommand>
{
    public CreateContactPageCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Message).NotEmpty();
    }
}
using FluentValidation;

namespace Application.Features.ContactPages.Commands.Update;

public class UpdateContactPageCommandValidator : AbstractValidator<UpdateContactPageCommand>
{
    public UpdateContactPageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Message).NotEmpty();
    }
}
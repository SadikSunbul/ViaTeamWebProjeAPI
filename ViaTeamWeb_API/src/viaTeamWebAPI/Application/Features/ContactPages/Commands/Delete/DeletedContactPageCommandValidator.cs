using FluentValidation;

namespace Application.Features.ContactPages.Commands.Delete;

public class DeleteContactPageCommandValidator : AbstractValidator<DeleteContactPageCommand>
{
    public DeleteContactPageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
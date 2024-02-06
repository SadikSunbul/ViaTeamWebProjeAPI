using FluentValidation;

namespace Application.Features.ExternalLinks.Commands.Delete;

public class DeleteExternalLinkCommandValidator : AbstractValidator<DeleteExternalLinkCommand>
{
    public DeleteExternalLinkCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
using FluentValidation;

namespace Application.Features.ExternalLinks.Commands.Create;

public class CreateExternalLinkCommandValidator : AbstractValidator<CreateExternalLinkCommand>
{
    public CreateExternalLinkCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Url).NotEmpty();
  
    }
}
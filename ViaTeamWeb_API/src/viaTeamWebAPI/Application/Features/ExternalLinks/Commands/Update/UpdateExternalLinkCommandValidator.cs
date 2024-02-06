using FluentValidation;

namespace Application.Features.ExternalLinks.Commands.Update;

public class UpdateExternalLinkCommandValidator : AbstractValidator<UpdateExternalLinkCommand>
{
    public UpdateExternalLinkCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Url).NotEmpty();
       
    }
}
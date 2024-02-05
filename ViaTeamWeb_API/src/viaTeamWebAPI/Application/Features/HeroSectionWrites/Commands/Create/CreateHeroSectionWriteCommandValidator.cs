using FluentValidation;

namespace Application.Features.HeroSectionWrites.Commands.Create;

public class CreateHeroSectionWriteCommandValidator : AbstractValidator<CreateHeroSectionWriteCommand>
{
    public CreateHeroSectionWriteCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Paragraph).NotEmpty();
        RuleFor(c => c.ButtonText).NotEmpty();
    }
}
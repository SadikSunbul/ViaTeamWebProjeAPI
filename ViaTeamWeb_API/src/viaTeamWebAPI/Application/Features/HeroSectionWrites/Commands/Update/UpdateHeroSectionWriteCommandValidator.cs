using FluentValidation;

namespace Application.Features.HeroSectionWrites.Commands.Update;

public class UpdateHeroSectionWriteCommandValidator : AbstractValidator<UpdateHeroSectionWriteCommand>
{
    public UpdateHeroSectionWriteCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Paragraph).NotEmpty();
        RuleFor(c => c.ButtonText).NotEmpty();
    }
}
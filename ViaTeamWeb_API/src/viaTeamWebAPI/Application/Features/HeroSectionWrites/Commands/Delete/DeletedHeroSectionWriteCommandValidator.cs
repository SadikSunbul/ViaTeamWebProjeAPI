using FluentValidation;

namespace Application.Features.HeroSectionWrites.Commands.Delete;

public class DeleteHeroSectionWriteCommandValidator : AbstractValidator<DeleteHeroSectionWriteCommand>
{
    public DeleteHeroSectionWriteCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
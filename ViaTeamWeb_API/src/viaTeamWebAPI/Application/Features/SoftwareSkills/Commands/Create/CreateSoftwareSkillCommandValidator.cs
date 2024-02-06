using FluentValidation;

namespace Application.Features.SoftwareSkills.Commands.Create;

public class CreateSoftwareSkillCommandValidator : AbstractValidator<CreateSoftwareSkillCommand>
{
    public CreateSoftwareSkillCommandValidator()
    {
        RuleFor(c => c.SkillName).NotEmpty();
        RuleFor(c => c.SkillPercent).NotEmpty();
    }
}
using FluentValidation;

namespace Application.Features.SoftwareSkills.Commands.Update;

public class UpdateSoftwareSkillCommandValidator : AbstractValidator<UpdateSoftwareSkillCommand>
{
    public UpdateSoftwareSkillCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.SkillName).NotEmpty();
        RuleFor(c => c.SkillPercent).NotEmpty();
    }
}
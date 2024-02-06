using FluentValidation;

namespace Application.Features.SoftwareSkills.Commands.Delete;

public class DeleteSoftwareSkillCommandValidator : AbstractValidator<DeleteSoftwareSkillCommand>
{
    public DeleteSoftwareSkillCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
using FluentValidation;

namespace Application.Features.SoftwareSkillMembers.Commands.Delete;

public class DeleteSoftwareSkillMemberCommandValidator : AbstractValidator<DeleteSoftwareSkillMemberCommand>
{
    public DeleteSoftwareSkillMemberCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
using FluentValidation;

namespace Application.Features.SoftwareSkillMembers.Commands.Update;

public class UpdateSoftwareSkillMemberCommandValidator : AbstractValidator<UpdateSoftwareSkillMemberCommand>
{
    public UpdateSoftwareSkillMemberCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.SoftwareSkillId).NotEmpty();
        RuleFor(c => c.MemberId).NotEmpty();
        RuleFor(c => c.SoftwareSkill).NotEmpty();
        RuleFor(c => c.Member).NotEmpty();
    }
}
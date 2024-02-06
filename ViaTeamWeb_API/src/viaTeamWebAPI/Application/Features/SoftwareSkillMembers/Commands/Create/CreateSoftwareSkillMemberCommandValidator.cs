using FluentValidation;

namespace Application.Features.SoftwareSkillMembers.Commands.Create;

public class CreateSoftwareSkillMemberCommandValidator : AbstractValidator<CreateSoftwareSkillMemberCommand>
{
    public CreateSoftwareSkillMemberCommandValidator()
    {
        RuleFor(c => c.SoftwareSkillId).NotEmpty();
     
    }
}
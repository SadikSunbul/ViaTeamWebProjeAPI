using FluentValidation;

namespace Application.Features.TeamMemberPresentations.Commands.Create;

public class CreateTeamMemberPresentationCommandValidator : AbstractValidator<CreateTeamMemberPresentationCommand>
{
    public CreateTeamMemberPresentationCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Prg1).NotEmpty();
        RuleFor(c => c.Prg2).NotEmpty();
    }
}
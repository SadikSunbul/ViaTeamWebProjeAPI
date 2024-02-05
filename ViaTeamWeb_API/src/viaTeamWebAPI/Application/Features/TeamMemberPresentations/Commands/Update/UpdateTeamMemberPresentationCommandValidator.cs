using FluentValidation;

namespace Application.Features.TeamMemberPresentations.Commands.Update;

public class UpdateTeamMemberPresentationCommandValidator : AbstractValidator<UpdateTeamMemberPresentationCommand>
{
    public UpdateTeamMemberPresentationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Prg1).NotEmpty();
        RuleFor(c => c.Prg2).NotEmpty();
    }
}
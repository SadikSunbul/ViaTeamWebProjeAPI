using FluentValidation;

namespace Application.Features.TeamAbouts.Commands.Update;

public class UpdateTeamAboutCommandValidator : AbstractValidator<UpdateTeamAboutCommand>
{
    public UpdateTeamAboutCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Title1).NotEmpty();
        RuleFor(c => c.Prg1).NotEmpty();
        RuleFor(c => c.Title2).NotEmpty();
        RuleFor(c => c.Prg2).NotEmpty();
    }
}
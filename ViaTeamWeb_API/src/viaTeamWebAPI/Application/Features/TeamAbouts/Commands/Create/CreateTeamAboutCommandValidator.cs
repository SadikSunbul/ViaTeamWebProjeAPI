using FluentValidation;

namespace Application.Features.TeamAbouts.Commands.Create;

public class CreateTeamAboutCommandValidator : AbstractValidator<CreateTeamAboutCommand>
{
    public CreateTeamAboutCommandValidator()
    {
        RuleFor(c => c.Title1).NotEmpty();
        RuleFor(c => c.Prg1).NotEmpty();
        RuleFor(c => c.Title2).NotEmpty();
        RuleFor(c => c.Prg2).NotEmpty();
    }
}
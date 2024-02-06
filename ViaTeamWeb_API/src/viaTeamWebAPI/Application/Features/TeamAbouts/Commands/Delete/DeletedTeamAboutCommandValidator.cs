using FluentValidation;

namespace Application.Features.TeamAbouts.Commands.Delete;

public class DeleteTeamAboutCommandValidator : AbstractValidator<DeleteTeamAboutCommand>
{
    public DeleteTeamAboutCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
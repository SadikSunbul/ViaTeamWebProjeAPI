using FluentValidation;

namespace Application.Features.TeamMemberPresentations.Commands.Delete;

public class DeleteTeamMemberPresentationCommandValidator : AbstractValidator<DeleteTeamMemberPresentationCommand>
{
    public DeleteTeamMemberPresentationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
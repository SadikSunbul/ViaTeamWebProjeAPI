using FluentValidation;

namespace Application.Features.TeamMembers.Commands.Delete;

public class DeleteTeamMemberCommandValidator : AbstractValidator<DeleteTeamMemberCommand>
{
    public DeleteTeamMemberCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
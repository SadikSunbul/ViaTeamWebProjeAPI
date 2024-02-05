using FluentValidation;

namespace Application.Features.Members.Commands.Create;

public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Job).NotEmpty();
        RuleFor(c => c.Country).NotEmpty();
        RuleFor(c => c.Authirize).NotEmpty();
        RuleFor(c => c.TeamMember).NotEmpty();
        RuleFor(c => c.User).NotEmpty();
    }
}
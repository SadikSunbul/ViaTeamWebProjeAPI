using FluentValidation;

namespace Application.Features.Members.Commands.Update;

public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Job).NotEmpty();
        RuleFor(c => c.Country).NotEmpty();
        RuleFor(c => c.Authirize).NotEmpty();
    }
}
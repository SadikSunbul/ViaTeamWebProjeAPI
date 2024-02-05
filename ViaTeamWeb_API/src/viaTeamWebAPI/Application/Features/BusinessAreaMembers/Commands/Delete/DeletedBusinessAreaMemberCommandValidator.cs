using FluentValidation;

namespace Application.Features.BusinessAreaMembers.Commands.Delete;

public class DeleteBusinessAreaMemberCommandValidator : AbstractValidator<DeleteBusinessAreaMemberCommand>
{
    public DeleteBusinessAreaMemberCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
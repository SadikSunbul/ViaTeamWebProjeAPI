using FluentValidation;

namespace Application.Features.Alooos.Commands.Delete;

public class DeleteAloooCommandValidator : AbstractValidator<DeleteAloooCommand>
{
    public DeleteAloooCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
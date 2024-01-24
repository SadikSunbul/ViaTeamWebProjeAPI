using FluentValidation;

namespace Application.Features.Tests.Commands.Delete;

public class DeleteTestCommandValidator : AbstractValidator<DeleteTestCommand>
{
    public DeleteTestCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
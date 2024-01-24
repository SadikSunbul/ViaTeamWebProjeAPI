using FluentValidation;

namespace Application.Features.Tests.Commands.Create;

public class CreateTestCommandValidator : AbstractValidator<CreateTestCommand>
{
    public CreateTestCommandValidator()
    {
        RuleFor(c => c.NAME).NotEmpty();
    }
}
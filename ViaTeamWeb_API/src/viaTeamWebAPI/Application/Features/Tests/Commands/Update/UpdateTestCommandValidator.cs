using FluentValidation;

namespace Application.Features.Tests.Commands.Update;

public class UpdateTestCommandValidator : AbstractValidator<UpdateTestCommand>
{
    public UpdateTestCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.NAME).NotEmpty();
    }
}
using FluentValidation;

namespace Application.Features.Alooos.Commands.Create;

public class CreateAloooCommandValidator : AbstractValidator<CreateAloooCommand>
{
    public CreateAloooCommandValidator()
    {
        RuleFor(c => c.Deneme).NotEmpty();
    }
}
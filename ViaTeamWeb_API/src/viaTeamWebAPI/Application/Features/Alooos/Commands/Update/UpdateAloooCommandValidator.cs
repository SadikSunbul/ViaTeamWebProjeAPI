using FluentValidation;

namespace Application.Features.Alooos.Commands.Update;

public class UpdateAloooCommandValidator : AbstractValidator<UpdateAloooCommand>
{
    public UpdateAloooCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Deneme).NotEmpty();
    }
}
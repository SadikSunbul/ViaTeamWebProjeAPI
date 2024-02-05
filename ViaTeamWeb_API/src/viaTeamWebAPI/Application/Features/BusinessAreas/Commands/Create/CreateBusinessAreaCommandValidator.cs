using FluentValidation;

namespace Application.Features.BusinessAreas.Commands.Create;

public class CreateBusinessAreaCommandValidator : AbstractValidator<CreateBusinessAreaCommand>
{
    public CreateBusinessAreaCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}
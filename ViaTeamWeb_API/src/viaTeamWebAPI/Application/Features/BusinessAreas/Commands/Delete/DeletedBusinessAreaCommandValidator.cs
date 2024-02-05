using FluentValidation;

namespace Application.Features.BusinessAreas.Commands.Delete;

public class DeleteBusinessAreaCommandValidator : AbstractValidator<DeleteBusinessAreaCommand>
{
    public DeleteBusinessAreaCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
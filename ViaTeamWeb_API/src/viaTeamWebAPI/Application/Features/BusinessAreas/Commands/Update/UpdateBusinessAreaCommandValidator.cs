using FluentValidation;

namespace Application.Features.BusinessAreas.Commands.Update;

public class UpdateBusinessAreaCommandValidator : AbstractValidator<UpdateBusinessAreaCommand>
{
    public UpdateBusinessAreaCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}
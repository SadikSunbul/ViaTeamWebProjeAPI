using FluentValidation;

namespace Application.Features.BusinessAreaMembers.Commands.Create;

public class CreateBusinessAreaMemberCommandValidator : AbstractValidator<CreateBusinessAreaMemberCommand>
{
    public CreateBusinessAreaMemberCommandValidator()
    {
        RuleFor(c => c.BusinessAreaId).NotEmpty();
      
    }
}
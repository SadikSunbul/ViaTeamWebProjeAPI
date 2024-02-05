using FluentValidation;

namespace Application.Features.BusinessAreaMembers.Commands.Update;

public class UpdateBusinessAreaMemberCommandValidator : AbstractValidator<UpdateBusinessAreaMemberCommand>
{
    public UpdateBusinessAreaMemberCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BusinessAreaId).NotEmpty();
        
    }
}
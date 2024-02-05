using FluentValidation;

namespace Application.Features.FeaturedSectionEntities.Commands.Delete;

public class DeleteFeaturedSectionEntitieCommandValidator : AbstractValidator<DeleteFeaturedSectionEntitieCommand>
{
    public DeleteFeaturedSectionEntitieCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
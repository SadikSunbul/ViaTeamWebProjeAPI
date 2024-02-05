using FluentValidation;

namespace Application.Features.FeaturedSectionEntities.Commands.Update;

public class UpdateFeaturedSectionEntitieCommandValidator : AbstractValidator<UpdateFeaturedSectionEntitieCommand>
{
    public UpdateFeaturedSectionEntitieCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.SmallTitle).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
    }
}
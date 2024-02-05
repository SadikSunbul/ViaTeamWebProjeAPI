using FluentValidation;

namespace Application.Features.FeaturedSectionEntities.Commands.Create;

public class CreateFeaturedSectionEntitieCommandValidator : AbstractValidator<CreateFeaturedSectionEntitieCommand>
{
    public CreateFeaturedSectionEntitieCommandValidator()
    {
        RuleFor(c => c.SmallTitle).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
    }
}
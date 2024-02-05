using FluentValidation;

namespace Application.Features.FeaturedArticleCards.Commands.Update;

public class UpdateFeaturedArticleCardCommandValidator : AbstractValidator<UpdateFeaturedArticleCardCommand>
{
    public UpdateFeaturedArticleCardCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Explanation).NotEmpty();
        RuleFor(c => c.Writer).NotEmpty();
        RuleFor(c => c.FeaturedSectionEntitiesId).NotEmpty();
    }
}
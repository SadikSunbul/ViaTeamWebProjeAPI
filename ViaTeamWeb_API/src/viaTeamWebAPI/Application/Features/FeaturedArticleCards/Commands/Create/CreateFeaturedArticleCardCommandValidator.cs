using FluentValidation;

namespace Application.Features.FeaturedArticleCards.Commands.Create;

public class CreateFeaturedArticleCardCommandValidator : AbstractValidator<CreateFeaturedArticleCardCommand>
{
    public CreateFeaturedArticleCardCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Explanation).NotEmpty();
        RuleFor(c => c.Writer).NotEmpty();
        RuleFor(c => c.FeaturedSectionEntitiesId).NotEmpty();
    }
}
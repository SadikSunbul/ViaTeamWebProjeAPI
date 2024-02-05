using FluentValidation;

namespace Application.Features.FeaturedArticleCards.Commands.Delete;

public class DeleteFeaturedArticleCardCommandValidator : AbstractValidator<DeleteFeaturedArticleCardCommand>
{
    public DeleteFeaturedArticleCardCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
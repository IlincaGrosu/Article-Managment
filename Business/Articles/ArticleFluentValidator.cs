using Business.Articles.Requests;
using FluentValidation;

namespace Business.Articles
{
    public class CreateArticleFluentValidator : AbstractValidator<CreateArticleRequest>
    {
        public CreateArticleFluentValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(5, 100).WithMessage("Title must be between 5 and 100 characters.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");

            RuleFor(x => x.PublishedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Published date cannot be in the future.");
        }
    }
    public class UpdateArticleFluentValidator : AbstractValidator<UpdateArticleRequest>
    {
        public UpdateArticleFluentValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(5, 100).WithMessage("Title must be between 5 and 100 characters.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");

            RuleFor(x => x.PublishedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Published date cannot be in the future.");
        }
    }
}

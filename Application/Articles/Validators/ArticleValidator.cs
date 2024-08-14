using Core.Articles;

namespace Application.Articles.Validators
{
    public interface IArticleValidator
    {
        Task<ValidationResult> Validate(Article article);
    }
    public class ArticleValidator : IArticleValidator
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleValidator(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<ValidationResult> Validate(Article article)
        {
            var validationResult = new ValidationResult();

            var existingTitle = _articleRepository.GetAll().Any(x => x.Title == article.Title);
            if (existingTitle)
            {
                validationResult.Errors.Add("This title already exists.");
            }

            return validationResult;
        }
    }

    public class ValidationResult
    {
        public List<string> Errors { get; set; } = new List<string>();
        public bool IsValid => Errors.Count == 0;
    }
}

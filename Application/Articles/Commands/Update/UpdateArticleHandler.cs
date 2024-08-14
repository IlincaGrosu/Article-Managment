using Application.Articles.Validators;
using AutoMapper;
using Core.Articles;
using MediatR;

namespace Application.Articles.Commands.Update
{
    public class UpdateArticleHandler : IRequestHandler<UpdateArticleCommand, Article>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleValidator _articleValidator;
        private readonly IMapper _mapper;

        public UpdateArticleHandler(IArticleRepository articleRepository, IArticleValidator articleValidator, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _articleValidator = articleValidator;
            _mapper = mapper;
        }

        public async Task<Article> Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
        {
            var article = _mapper.Map<Article>(command);

            await Validate(article, command, cancellationToken);

            await _articleRepository.UpdateAsync(article, cancellationToken);

            return article;
        }

        private async Task Validate(Article article, UpdateArticleCommand command, CancellationToken cancellationToken)
        {
            var existingArticle = _articleRepository.GetAll().Any(x => x.Id == command.Id);

            if (!existingArticle)
            {
                throw new ArgumentException("The article you want to update does not exist");
            }

            var validationResult = await _articleValidator.Validate(article);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException(string.Join(", ", validationResult.Errors));
            }
        }
    }
}

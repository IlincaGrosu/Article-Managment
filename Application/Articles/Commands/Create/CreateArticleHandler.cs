using Application.Articles.Validators;
using AutoMapper;
using Core.Articles;
using MediatR;

namespace Application.Articles.Commands.Create
{
    public class CreateArticleHandler : IRequestHandler<CreateArticleCommand, int>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleValidator _articleValidator;
        private readonly IMapper _mapper;

        public CreateArticleHandler(IArticleRepository repository, IArticleValidator articleValidator, IMapper mapper)
        {
            _articleRepository = repository;
            _articleValidator = articleValidator;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
        {
            var article = _mapper.Map<Article>(command);
            var validationResult = await _articleValidator.Validate(article);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException(string.Join(", ", validationResult.Errors));
            }

            await _articleRepository.AddAsync(article, cancellationToken);

            return article.Id;
        }
    }
}

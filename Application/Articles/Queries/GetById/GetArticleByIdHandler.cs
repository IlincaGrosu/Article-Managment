using Core.Articles;
using MediatR;

namespace Application.Articles.Queries.GetById
{
    public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdQuery, Article>
    {
        private readonly IArticleRepository _articleRepository;

        public GetArticleByIdHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<Article> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _articleRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new ArgumentException("No article with given id");
        }
    }
}

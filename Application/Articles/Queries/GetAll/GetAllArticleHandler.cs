using Application.Articles.DTOs;
using AutoMapper;
using Core.Articles;
using Infrastructure.Articles.DTOs;
using MediatR;

namespace Application.Articles.Queries.GetAll
{
    public class GetAllArticleHandler : IRequestHandler<GetAllArticleQuery, PaginatedResult<ArticleDto>>
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;

        public GetAllArticleHandler(IArticleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ArticleDto>> Handle(GetAllArticleQuery request, CancellationToken cancellationToken)
        {
            var articles = _repository.GetAll();
            var totalItems = articles.Count();

            var pagedArticles = articles
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var articleDtos = _mapper.Map<IEnumerable<ArticleDto>>(pagedArticles);

            return new PaginatedResult<ArticleDto>(articleDtos, totalItems, request.Page, request.PageSize);
        }
    }
}

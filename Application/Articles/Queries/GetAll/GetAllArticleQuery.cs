using Application.Articles.DTOs;
using Infrastructure.Articles.DTOs;
using MediatR;

namespace Application.Articles.Queries.GetAll
{
    public class GetAllArticleQuery : IRequest<PaginatedResult<ArticleDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public GetAllArticleQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}

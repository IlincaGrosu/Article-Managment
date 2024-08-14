using Core.Articles;
using MediatR;

namespace Application.Articles.Queries.GetById
{
    public class GetArticleByIdQuery : IRequest<Article>
    {
        public int Id { get; set; }

        public GetArticleByIdQuery(int id)
        {
            Id = id;
        }
    }
}

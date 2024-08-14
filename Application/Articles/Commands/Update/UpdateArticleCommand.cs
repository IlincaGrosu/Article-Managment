using Core.Articles;
using MediatR;

namespace Application.Articles.Commands.Update
{
    public class UpdateArticleCommand : IRequest<Article>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}

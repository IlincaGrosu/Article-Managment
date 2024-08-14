using MediatR;

namespace Application.Articles.Commands.Create
{
    public class CreateArticleCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }

        public CreateArticleCommand(string title, string content, DateTime publishedDate)
        {
            Title = title;
            Content = content;
            PublishedDate = publishedDate;
        }
    }
}

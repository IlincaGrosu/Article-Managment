namespace Application.Articles.Commands.Delete
{
    public class DeleteArticleCommand : MediatR.IRequest<int>
    {
        public int Id { get; set; }
    }
}

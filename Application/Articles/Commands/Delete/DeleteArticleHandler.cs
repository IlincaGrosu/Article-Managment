using Core.Articles;
using MediatR;

namespace Application.Articles.Commands.Delete
{
    public class DeleteArticleHandler : IRequestHandler<DeleteArticleCommand, int>
    {
        private readonly IArticleRepository _articleRepository;

        public DeleteArticleHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<int> Handle(DeleteArticleCommand command, CancellationToken cancellationToken)
        {
            var existingArticle = _articleRepository.GetAll().Any(x => x.Id == command.Id);

            if (!existingArticle)
            {
                throw new ArgumentException("The article you want to delete does not exist");
            }

            await _articleRepository.DeleteAsync(command.Id, cancellationToken);
            return command.Id;
        }
    }
}

namespace Core.Articles
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
    }

    public interface IArticleRepository
    {
        IEnumerable<Article> GetAll();
        Task<Article> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Article article, CancellationToken cancellationToken);
        Task UpdateAsync(Article article, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}

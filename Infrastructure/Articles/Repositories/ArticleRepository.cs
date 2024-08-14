using Core.Articles;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Articles.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Article> GetAll() => _context.Articles.AsEnumerable();

        public async Task<Article> GetByIdAsync(int id, CancellationToken cancellationToken) => await _context.Articles.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

        public async Task AddAsync(Article article, CancellationToken cancellationToken)
        {
            article.Id = _context.Articles.Any() ? _context.Articles.Max(a => a.Id) + 1 : 1;
            _context.Articles.Add(article);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Article article, CancellationToken cancellationToken)
        {
            var existingArticle = await GetByIdAsync(article.Id, cancellationToken);
            if (existingArticle != null)
            {
                existingArticle.Title = article.Title;
                existingArticle.Content = article.Content;
                existingArticle.PublishedDate = article.PublishedDate;
                _context.Articles.Update(existingArticle);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var article = await GetByIdAsync(id, cancellationToken);
            if (article != null)
            {
                article.Active = false;
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

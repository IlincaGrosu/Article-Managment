namespace Infrastructure.Articles.DTOs
{
    public class ArticleDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool Active { get; set; }

    }
}

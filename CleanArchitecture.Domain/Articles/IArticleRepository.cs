namespace CleanArchitecture.Domain.Articles
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllArticlesAsync();
        Task<Article> CreateArticleAsync(Article article);
    }
}

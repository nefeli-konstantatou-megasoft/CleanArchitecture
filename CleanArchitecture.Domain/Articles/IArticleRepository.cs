namespace CleanArchitecture.Domain.Articles;

public interface IArticleRepository
{
    Task<List<Article>> GetAllArticlesAsync();
    Task<List<Article>> GetAllPublishedArticlesAsync();
    Task<List<Article>> GetArticlesByUserId(string userId);
    Task<List<Article>> GetArticlesByUserName(string username);
    Task<Article> CreateArticleAsync(Article article);
    Task<Article?> GetArticleByIdAsync(int id);
    Task<Article?> UpdateArticleAsync(Article article);
    Task<bool> UpdateArticlePublishAsync(int id, bool isPublished);
    Task<bool> DeleteArticleByIdAsync(int id);
}

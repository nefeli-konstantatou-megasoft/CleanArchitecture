namespace CleanArchitecture.Domain.Articles;

public interface IArticleRepository
{
    Task<List<Article>> GetAllArticlesAsync();
    Task<List<Article>> GetAllPublishedArticlesAsync();
    Task<Article> CreateArticleAsync(Article article);
    Task<Article?> GetArticleByIdAsync(int id);
    Task<Article?> UpdateArticleAsync(Article article);
    Task<bool> DeleteArticleByIdAsync(int id);
}

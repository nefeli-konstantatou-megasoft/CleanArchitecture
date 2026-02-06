namespace CleanArchitecture.Domain.Articles
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllArticlesAsync();
    }
}

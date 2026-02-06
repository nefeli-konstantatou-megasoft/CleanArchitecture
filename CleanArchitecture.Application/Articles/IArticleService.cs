using CleanArchitecture.Domain.Articles;

namespace CleanArchitecture.Application.Articles
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticlesAsync();
    }
}

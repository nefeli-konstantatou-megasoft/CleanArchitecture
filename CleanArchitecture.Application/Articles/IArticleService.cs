using CleanArchitecture.Domain.Articles;

namespace CleanArchitecture.Application.Articles
{
    public interface IArticleService
    {
        List<Article> GetAllArticles();
    }
}

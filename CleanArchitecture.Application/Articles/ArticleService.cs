using CleanArchitecture.Domain.Articles;

namespace CleanArchitecture.Application.Articles
{
    public class ArticleService : IArticleService
    {
        public List<Article> GetAllArticles()
        {
            return
            [
                new() { Id = 1, Title = "My first article!", Content = "Lorem ipsum dolor sit amet" },
                new() { Id = 2, Title = "My second article!", Content = "Foo bar baz foobar barfoo" },
                new() { Id = 3, Title = "My third article!", Content = "Hello world goodbye world greetings world farewell world" },
            ];
        }
    }
}

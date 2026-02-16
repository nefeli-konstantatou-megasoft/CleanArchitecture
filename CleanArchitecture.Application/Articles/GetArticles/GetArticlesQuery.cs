using MediatR;

namespace CleanArchitecture.Application.Articles.GetArticles
{
    public class GetArticlesQuery : IRequest<List<ArticleResponse>>
    {
    }
}

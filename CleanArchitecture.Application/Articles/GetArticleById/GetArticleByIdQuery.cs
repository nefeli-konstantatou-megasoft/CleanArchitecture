using MediatR;

namespace CleanArchitecture.Application.Articles.GetArticleById
{
    public class GetArticleByIdQuery(int id) : IRequest<ArticleResponse?>
    {
        public int Id { get; set; } = id;
    }
}

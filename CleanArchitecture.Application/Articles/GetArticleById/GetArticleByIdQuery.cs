namespace CleanArchitecture.Application.Articles.GetArticleById;

public class GetArticleByIdQuery(int id) : IQuery<ArticleResponse?>
{
    public int Id { get; set; } = id;
}

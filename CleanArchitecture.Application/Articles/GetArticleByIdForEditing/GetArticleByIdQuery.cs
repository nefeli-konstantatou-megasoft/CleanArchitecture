namespace CleanArchitecture.Application.Articles.GetArticleById;

public class GetArticleByIdForEditingQuery(int id) : IQuery<ArticleResponse>
{
    public int Id { get; set; } = id;
}

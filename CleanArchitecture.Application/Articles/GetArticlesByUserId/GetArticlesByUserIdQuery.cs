namespace CleanArchitecture.Application.Articles.GetArticlesByUserId;

public class GetArticlesByUserIdQuery(string userId) : IQuery<List<ArticleResponse>>
{
    public string UserId { get; set; } = userId;
}

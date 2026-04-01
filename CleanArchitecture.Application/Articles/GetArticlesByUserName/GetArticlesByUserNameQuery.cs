namespace CleanArchitecture.Application.Articles.GetArticlesByUserName;

public class GetArticlesByUserNameQuery(string userId) : IQuery<List<ArticleResponse>>
{
    public string UserId { get; set; } = userId;
}

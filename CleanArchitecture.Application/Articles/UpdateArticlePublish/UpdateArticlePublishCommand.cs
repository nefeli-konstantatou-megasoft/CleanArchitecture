namespace CleanArchitecture.Application.Articles.UpdateArticlePublish;

public class UpdateArticlePublishCommand(int articleId, bool isPublished) : ICommand
{
    public int ArticleId { get; set; } = articleId;
    public bool IsPublished { get; set; } = isPublished;
}

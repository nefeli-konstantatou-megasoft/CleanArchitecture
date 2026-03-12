namespace CleanArchitecture.Application.Articles.CreateArticle;

public class CreateArticleCommand : ICommand<ArticleResponse>
{
    public required string Title { get; set; }
    public string? Content { get; set; }
    public DateTime DatePublished { get; set; } = DateTime.Now;
    public bool IsPublished { get; set; }
    public string? UserId { get; set; } = null;
}

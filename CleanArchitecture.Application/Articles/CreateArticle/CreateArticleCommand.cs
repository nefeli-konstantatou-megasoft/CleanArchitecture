using MediatR;

namespace CleanArchitecture.Application.Articles.CreateArticle
{
    public class CreateArticleCommand : IRequest<ArticleResponse>
    {
        public required string Title { get; set; }
        public string? Content { get; set; }
        public DateTime DatePublished { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; }
    }
}

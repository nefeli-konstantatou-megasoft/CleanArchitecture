namespace CleanArchitecture.Application.Articles.UpdateArticle;

public class UpdateArticleCommandHandler : ICommandHandler<UpdateArticleCommand, ArticleResponse?>
{
    public readonly IArticleRepository _articleRepository;
    public UpdateArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Result<ArticleResponse?>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var updatedArticle = request.Adapt<Article>();
        var article = await _articleRepository.UpdateArticleAsync(updatedArticle);
        return article?.Adapt<ArticleResponse>() ?? null;
    }
}

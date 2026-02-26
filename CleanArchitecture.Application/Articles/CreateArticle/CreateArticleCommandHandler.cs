namespace CleanArchitecture.Application.Articles.CreateArticle;

public class CreateArticleCommandHandler : ICommandHandler<CreateArticleCommand, ArticleResponse>
{
    private readonly IArticleRepository _articleRepository;

    public CreateArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Result<ArticleResponse>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = request.Adapt<Article>();
        var newArticle = await _articleRepository.CreateArticleAsync(article);
        return newArticle.Adapt<ArticleResponse>();
    }
}

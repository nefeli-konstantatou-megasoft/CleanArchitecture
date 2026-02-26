namespace CleanArchitecture.Application.Articles.GetArticleById;

public class GetArticleByIdQueryHandler : IQueryHandler<GetArticleByIdQuery, ArticleResponse?>
{
    private readonly IArticleRepository _articleRepository;

    public GetArticleByIdQueryHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Result<ArticleResponse?>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _articleRepository.GetArticleByIdAsync(request.Id);
        return result is null
            ? Result<ArticleResponse?>.Error(ArticleErrors.ArticleNotFound)
            : result.Adapt<ArticleResponse>();
    }
}

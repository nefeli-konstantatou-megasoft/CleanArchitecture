namespace CleanArchitecture.Application.Articles.GetArticles;

public class GetArticlesQueryHandler : IQueryHandler<GetArticlesQuery, List<ArticleResponse>>
{
    private readonly IArticleRepository _articleRepository;
    public GetArticlesQueryHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Result<List<ArticleResponse>>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await _articleRepository.GetAllArticlesAsync();
        return articles.Adapt<List<ArticleResponse>>();
    }
}

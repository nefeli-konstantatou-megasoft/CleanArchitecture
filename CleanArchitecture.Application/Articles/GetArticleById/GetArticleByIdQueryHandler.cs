using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Articles.GetArticleById;

public class GetArticleByIdQueryHandler(
    IArticleRepository articleRepository,
    IUserRepository userRepository) : IQueryHandler<GetArticleByIdQuery, ArticleResponse?>
{
    private readonly IArticleRepository _articleRepository = articleRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<ArticleResponse?>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetArticleByIdAsync(request.Id);

        if (article is null)
            return Result<ArticleResponse?>.Error(ArticleErrors.ArticleNotFound);

        var articleResponse = article.Adapt<ArticleResponse>();

        if(article.UserId is not null)
        {
            var author = await _userRepository.GetUserByIdAsync(article.UserId);
            articleResponse.UserName = author?.UserName ?? "Unknown";
        }
        else
        {
            articleResponse.UserName = "Unknown";
        }

        return articleResponse;
    }
}

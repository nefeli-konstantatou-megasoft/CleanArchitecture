using CleanArchitecture.Application.User;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Articles.GetArticleById;

public class GetArticleByIdQueryHandler(
    IArticleRepository articleRepository,
    IUserRepository userRepository,
    IUserService userService) : IQueryHandler<GetArticleByIdQuery, ArticleResponse?>
{
    private readonly IArticleRepository _articleRepository = articleRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserService _userService = userService;

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
            articleResponse.UserId = article.UserId;
            articleResponse.CanEdit = await _userService.UserCanEditArticleAsync(article.Id);
        }
        else
        {
            articleResponse.UserName = "Unknown";
            articleResponse.CanEdit = false;
        }

        return articleResponse;
    }
}

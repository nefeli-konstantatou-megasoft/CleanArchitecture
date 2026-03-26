using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Users;

namespace CleanArchitecture.Application.Articles.CreateArticle;

public class CreateArticleCommandHandler(
    IArticleRepository articleRepository,
    IUserService userService) : ICommandHandler<CreateArticleCommand, ArticleResponse>
{
    private readonly IArticleRepository _articleRepository = articleRepository;
    private readonly IUserService _userService = userService;

    public async Task<Result<ArticleResponse>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var article = request.Adapt<Article>();
            article.UserId = await _userService.GetUserIdAsync();

            if (!await _userService.UserCanCreateArticlesAsync())
                return UnauthorizedError;

            var newArticle = await _articleRepository.CreateArticleAsync(article);
            return newArticle.Adapt<ArticleResponse>();
        }
        catch(UserUnauthorizedException)
        {
            return UnauthorizedError;
        }
    }

    private Result<ArticleResponse> UnauthorizedError => Result<ArticleResponse>.Error(ArticleErrors.ArticleCreationUnauthorized);
}

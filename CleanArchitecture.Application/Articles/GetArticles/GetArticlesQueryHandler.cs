using CleanArchitecture.Application.Users;
using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Articles.GetArticles;

public class GetArticlesQueryHandler(
    IArticleRepository articleRepository,
    IUserRepository userRepository,
    IUserService userService) : IQueryHandler<GetArticlesQuery, List<ArticleResponse>>
{
    private readonly IArticleRepository _articleRepository = articleRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserService _userService = userService;

    public async Task<Result<List<ArticleResponse>>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        List<Article> articles;

        if (await _userService.UserHasPermissionsAsync(RolePermissionFlags.ViewUnpublishedArticles))
            articles = await _articleRepository.GetAllArticlesAsync();
        else
            articles = await _articleRepository.GetAllPublishedArticlesAsync();

        List<ArticleResponse> response = [];

        foreach(var article in articles)
            response.Add(new ArticleResponse(
                article.Id,
                article.Title,
                article.Content,
                article.DatePublished,
                article.IsPublished,
                article.Author?.UserName ?? "Unknown",
                article.UserId ?? string.Empty,
                await _userService.UserCanEditArticleAsync(article.Id)
            ));

        return response;
    }
}

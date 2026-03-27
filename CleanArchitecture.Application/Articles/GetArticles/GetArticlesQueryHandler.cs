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

        foreach (var article in articles)
        {
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
            response.Add(articleResponse);
        }

        return response.OrderByDescending(article => article.DatePublished).ToList();
    }
}

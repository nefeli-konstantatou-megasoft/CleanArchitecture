using CleanArchitecture.Application.Users;

namespace CleanArchitecture.Application.Articles.GetArticlesByUserId;

public class GetArticlesByCurrentUserQueryHandler(
    IArticleRepository articleRepository,
    IUserService userService) : IQueryHandler<GetArticlesByCurrentUserQuery, List<ArticleResponse>>
{
    private readonly IArticleRepository _articleRepository = articleRepository;
    private readonly IUserService _userService = userService;

    public async Task<Result<List<ArticleResponse>>> Handle(GetArticlesByCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var articles = await _articleRepository.GetArticlesByUserId(await _userService.GetUserIdAsync());
        return articles.Adapt<List<ArticleResponse>>();
    }
}

using CleanArchitecture.Application.Users;

namespace CleanArchitecture.Application.Articles.GetArticlesByUserName;

public class GetArticlesByUserNameQueryHandler(
    IArticleRepository articleRepository,
    IUserService userService) : IQueryHandler<GetArticlesByUserNameQuery, List<ArticleResponse>>
{
    private readonly IArticleRepository _articleRepository = articleRepository;
    private readonly IUserService _userService = userService;

    public async Task<Result<List<ArticleResponse>>> Handle(GetArticlesByUserNameQuery request, CancellationToken cancellationToken)
    {
        var articles = await _articleRepository.GetArticlesByUserId(request.UserId);
        return articles.Adapt<List<ArticleResponse>>();
    }
}

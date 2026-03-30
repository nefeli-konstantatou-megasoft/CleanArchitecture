using CleanArchitecture.Application.Users;

namespace CleanArchitecture.Application.Articles.GetArticlesByUserId;

public class GetArticlesByUserIdQueryHandler(
    IArticleRepository articleRepository,
    IUserService userService) : IQueryHandler<GetArticlesByUserIdQuery, List<ArticleResponse>>
{
    private readonly IArticleRepository _articleRepository = articleRepository;
    private readonly IUserService _userService = userService;

    public async Task<Result<List<ArticleResponse>>> Handle(GetArticlesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var articles = await _articleRepository.GetArticlesByUserId(request.UserId);
        return articles.Adapt<List<ArticleResponse>>();
    }
}

using CleanArchitecture.Application.User;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Articles.GetArticleById;

public class GetArticleByIdForEditingQueryHandler(
    IArticleRepository articleRepository,
    IUserService userService) : IQueryHandler<GetArticleByIdForEditingQuery, ArticleResponse>
{
    private readonly IArticleRepository _articleRepository = articleRepository;
    private readonly IUserService _userService = userService;

    public async Task<Result<ArticleResponse>> Handle(GetArticleByIdForEditingQuery request, CancellationToken cancellationToken)
    {
        var canEdit = await _userService.UserCanEditArticleAsync(request.Id);

        if(!canEdit)
            return Result<ArticleResponse>.Error(ArticleErrors.ArticleEditUnauthorized);

        var article = await _articleRepository.GetArticleByIdAsync(request.Id);
        var articleResponse = article.Adapt<ArticleResponse>();

        return articleResponse;
    }
}

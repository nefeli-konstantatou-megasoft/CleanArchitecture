using CleanArchitecture.Application.User;

namespace CleanArchitecture.Application.Articles.UpdateArticle;

public class UpdateArticleCommandHandler(
    IArticleRepository articleRepository,
    IUserService userService) : ICommandHandler<UpdateArticleCommand, ArticleResponse?>
{
    public readonly IArticleRepository _articleRepository = articleRepository;
    public readonly IUserService _userService = userService;

    public async Task<Result<ArticleResponse?>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var updatedArticle = request.Adapt<Article>();
        if(!await _userService.UserCanEditArticleAsync(updatedArticle.Id))
            return Result<ArticleResponse?>.Error(ArticleErrors.ArticleEditUnauthorized);
            
        var article = await _articleRepository.UpdateArticleAsync(updatedArticle);

        if(article is null)
            return Result<ArticleResponse?>.Error(ArticleErrors.ArticleNotFound);

        return article?.Adapt<ArticleResponse>() ?? null;
    }
}

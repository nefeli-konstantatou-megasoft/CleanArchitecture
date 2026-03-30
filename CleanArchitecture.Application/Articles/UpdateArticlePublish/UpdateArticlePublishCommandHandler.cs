using CleanArchitecture.Application.Users;

namespace CleanArchitecture.Application.Articles.UpdateArticlePublish;

public class UpdateArticlePublishCommandHandler(
    IUserService userService,
    IArticleRepository articleRepository) : ICommandHandler<UpdateArticlePublishCommand>
{
    IUserService _userService = userService;
    IArticleRepository _articleRepository = articleRepository;

    public async Task<Result> Handle(UpdateArticlePublishCommand request, CancellationToken cancellationToken)
    {
        if (!await _userService.UserCanEditArticleAsync(request.ArticleId))
            return Result.Error(ArticleErrors.ArticleEditUnauthorized);

        var succeeded = await _articleRepository.UpdateArticlePublishAsync(request.ArticleId, request.IsPublished);
        var result = succeeded ? Result.Ok() : Result.Error(GeneralErrors.UnexpectedFailure);
        return result;
    }
}

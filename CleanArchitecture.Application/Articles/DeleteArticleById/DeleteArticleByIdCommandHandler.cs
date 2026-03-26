using CleanArchitecture.Application.Users;

namespace CleanArchitecture.Application.Articles.DeleteArticleById;

public class DeleteArticleByIdCommandHandler(
    IArticleRepository articleRepository,
    IUserService userService) : ICommandHandler<DeleteArticleByIdCommand>
{
    private readonly IArticleRepository _articleRepository = articleRepository;
    private readonly IUserService _userService = userService;

    public async Task<Result> Handle(DeleteArticleByIdCommand request, CancellationToken cancellationToken)
    {
        if (!await _userService.UserCanEditArticleAsync(request.Id))
            return Result.Error(ArticleErrors.ArticleEditUnauthorized);

        bool success = await _articleRepository.DeleteArticleByIdAsync(request.Id);
        return success ? Result.Ok() : Result.Error(ArticleErrors.ArticleNotFound);
    }
}

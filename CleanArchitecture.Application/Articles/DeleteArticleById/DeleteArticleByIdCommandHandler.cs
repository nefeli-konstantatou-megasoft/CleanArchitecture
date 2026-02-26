namespace CleanArchitecture.Application.Articles.DeleteArticleById;

public class DeleteArticleByIdCommandHandler : ICommandHandler<DeleteArticleByIdCommand>
{
    private readonly IArticleRepository _articleRepository;
    public DeleteArticleByIdCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Result> Handle(DeleteArticleByIdCommand request, CancellationToken cancellationToken)
    {
        bool success = await _articleRepository.DeleteArticleByIdAsync(request.Id);
        return success ? Result.Ok() : Result.Error(ArticleErrors.ArticleNotFound);
    }
}

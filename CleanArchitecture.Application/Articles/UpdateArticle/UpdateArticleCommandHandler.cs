using CleanArchitecture.Application.Articles.CreateArticle;
using CleanArchitecture.Domain.Articles;
using Mapster;
using MediatR;

namespace CleanArchitecture.Application.Articles.UpdateArticle
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ArticleResponse?>
    {
        public readonly IArticleRepository _articleRepository;
        public UpdateArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<ArticleResponse?> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var updatedArticle = request.Adapt<Article>();
            var article = await _articleRepository.UpdateArticleAsync(updatedArticle);
            return article?.Adapt<ArticleResponse>() ?? null;
        }
    }
}

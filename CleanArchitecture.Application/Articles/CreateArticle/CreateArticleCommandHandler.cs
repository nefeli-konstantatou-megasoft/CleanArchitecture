using CleanArchitecture.Domain.Articles;
using Mapster;
using MediatR;

namespace CleanArchitecture.Application.Articles.CreateArticle
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleResponse>
    {
        private readonly IArticleRepository _articleRepository;

        public CreateArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<ArticleResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = request.Adapt<Article>();
            var newArticle = await _articleRepository.CreateArticleAsync(article);
            return newArticle.Adapt<ArticleResponse>();
        }
    }
}

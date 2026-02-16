using CleanArchitecture.Domain.Articles;
using Mapster;
using MediatR;

namespace CleanArchitecture.Application.Articles.GetArticles
{
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, List<ArticleResponse>>
    {
        private readonly IArticleRepository _articleRepository;
        public GetArticlesQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<List<ArticleResponse>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await _articleRepository.GetAllArticlesAsync();
            return articles.Adapt<List<ArticleResponse>>();
        }
    }
}

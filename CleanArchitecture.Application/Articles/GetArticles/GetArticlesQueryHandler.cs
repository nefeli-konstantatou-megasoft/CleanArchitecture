using CleanArchitecture.Domain.Articles;
using MediatR;

namespace CleanArchitecture.Application.Articles.GetArticles
{
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, List<Article>>
    {
        private readonly IArticleRepository _articleRepository;
        public GetArticlesQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<List<Article>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await _articleRepository.GetAllArticlesAsync();
            return articles;
        }
    }
}

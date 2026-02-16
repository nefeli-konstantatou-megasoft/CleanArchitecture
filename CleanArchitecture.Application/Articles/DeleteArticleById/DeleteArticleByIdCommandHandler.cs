using CleanArchitecture.Domain.Articles;
using MediatR;

namespace CleanArchitecture.Application.Articles.DeleteArticle
{
    public class DeleteArticleByIdCommandHandler : IRequestHandler<DeleteArticleByIdCommand, bool>
    {
        private readonly IArticleRepository _articleRepository;
        public DeleteArticleByIdCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<bool> Handle(DeleteArticleByIdCommand request, CancellationToken cancellationToken)
        {
            return await _articleRepository.DeleteArticleByIdAsync(request.Id);
        }
    }
}

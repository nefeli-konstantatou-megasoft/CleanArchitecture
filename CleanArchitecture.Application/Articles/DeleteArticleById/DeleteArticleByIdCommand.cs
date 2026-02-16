using MediatR;

namespace CleanArchitecture.Application.Articles.DeleteArticle
{
    public class DeleteArticleByIdCommand(int id) : IRequest<bool>
    {
        public int Id { get; set; } = id;
    }
}

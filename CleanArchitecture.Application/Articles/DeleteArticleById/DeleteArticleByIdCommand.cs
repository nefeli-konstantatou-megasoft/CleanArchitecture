namespace CleanArchitecture.Application.Articles.DeleteArticleById;

public class DeleteArticleByIdCommand(int id) : ICommand
{
    public int Id { get; set; } = id;
}

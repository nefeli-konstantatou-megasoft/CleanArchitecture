using CleanArchitecture.Domain.Articles;

namespace CleanArchitecture.Domain.Users;

public interface IUser
{
    string Id { get; set; }
    string? UserName { get; set; }
    string? Email { get; set; }
    List<Article> Articles { get; set; }
}

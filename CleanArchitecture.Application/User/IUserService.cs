namespace CleanArchitecture.Application.User;

public interface IUserService
{
    Task<string> GetUserIdAsync();
    Task<bool> UserHasRoleAsync(string role);
    Task<bool> UserCanCreateArticlesAsync();
    Task<bool> UserCanEditArticleAsync(int articleId);
}

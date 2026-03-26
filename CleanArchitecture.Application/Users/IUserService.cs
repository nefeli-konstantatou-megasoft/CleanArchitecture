namespace CleanArchitecture.Application.Users;

public interface IUserService
{
    Task<string> GetUserIdAsync();
    Task<bool> UserHasRoleAsync(string role);
    Task<bool> UserCanCreateArticlesAsync();
    Task<bool> UserCanEditArticleAsync(int articleId);
    Task<List<string>> GetUserRolesByUserIdAsync(string userId);
    Task<ErrorMessage?> AddRoleToUserAsync(string userId, string roleName);
    Task<ErrorMessage?> RemoveRoleFromUserAsync(string userId, string roleName);
}

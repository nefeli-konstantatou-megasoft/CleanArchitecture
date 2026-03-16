using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.User;
using CleanArchitecture.Domain.Articles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Users;

public class UserService(
    UserManager<User> userManager,
    IHttpContextAccessor httpContextAccessor,
    IArticleRepository articleRepository) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IArticleRepository _articleRepository = articleRepository;
    const string AdminRole = "Admin";
    const string WriterRole = "Writer";

    public async Task<string> GetUserIdAsync()
    {
        var user = await GetUserAsync();
        return user?.Id ?? throw new UserUnauthorizedException();
    }

    public async Task<bool> UserCanCreateArticlesAsync()
    {
        var user = await GetUserAsync();
        if (user is null)
            return false;

        return await _userManager.IsInRoleAsync(user, AdminRole) || await _userManager.IsInRoleAsync(user, WriterRole);
    }

    public async Task<bool> UserCanEditArticleAsync(int articleId)
    {
        var user = await GetUserAsync();
        var article = await _articleRepository.GetArticleByIdAsync(articleId);

        if (user is null || article is null)
            return false;

        var isAuthor = async() => await _userManager.IsInRoleAsync(user, WriterRole) && article.UserId == user.Id;
        return await _userManager.IsInRoleAsync(user, AdminRole) || await isAuthor();
    }

    public async Task<bool> UserHasRoleAsync(string role)
    {
        var user = await GetUserAsync();
        return user is not null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<User?> GetUserAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.User is null)
            return null;

        var user = await _userManager.GetUserAsync(httpContext.User);
        return user;
    }
}

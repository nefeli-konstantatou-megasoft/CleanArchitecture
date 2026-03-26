using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Users;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Articles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Users;

public class UserService(
    UserManager<User> userManager,
    IHttpContextAccessor httpContextAccessor,
    IArticleRepository articleRepository,
    RoleManager<IdentityRole> roleManager) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IArticleRepository _articleRepository = articleRepository;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

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

    public async Task<List<string>> GetUserRolesByUserIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return [];

        var roles = await _userManager.GetRolesAsync(user);
        return roles.ToList();
    }

    public async Task<ErrorMessage?> AddRoleToUserAsync(string userId, string roleName)
    {
        if (!await UserHasRoleAsync(AdminRole))
            return UserErrors.UnauthorizedAction;

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return UserErrors.InvalidUserId;

        else if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var createRoleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!createRoleResult.Succeeded)
                return GeneralErrors.UnexpectedFailure;
        }

        var addRoleResult = await _userManager.AddToRoleAsync(user, roleName);

        if(!addRoleResult.Succeeded)
            return UserErrors.UserAlreadyHasRole;

        return null;
    }

    public async Task<ErrorMessage?> RemoveRoleFromUserAsync(string userId, string roleName)
    {
        if (!await UserHasRoleAsync(AdminRole))
            return UserErrors.UnauthorizedAction;

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return UserErrors.InvalidUserId;

        else if (!await _roleManager.RoleExistsAsync(roleName))
            return UserErrors.RoleNameNotFound;

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        if (!result.Succeeded)
            return GeneralErrors.UnexpectedFailure;

        return null;
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

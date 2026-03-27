using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Users;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Articles;
using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Infrastructure.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Users;

public class UserService(
    UserManager<User> userManager,
    IHttpContextAccessor httpContextAccessor,
    IArticleRepository articleRepository,
    RoleManager<Role> roleManager,
    IUserRepository userRepository) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IArticleRepository _articleRepository = articleRepository;
    private readonly RoleManager<Role> _roleManager = roleManager;
    private readonly IUserRepository _userRepository = userRepository;

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

        var permissions = await _userRepository.GetUserPermissionsByUserIdAsync(user.Id);
        if (permissions is null)
            return false;

        return permissions.Value[RolePermissionFlags.CreateArticles];
    }

    public async Task<bool> UserCanEditArticleAsync(int articleId)
    {
        (var user, var permissions) = await GetUserWithPermissionsAsync();

        if (user is null || permissions is null)
            return false;

        var article = await _articleRepository.GetArticleByIdAsync(articleId);

        if (article is null)
            return false;

        if (article.UserId == user.Id)
            return permissions.Value[RolePermissionFlags.EditOwnArticles];

        return permissions.Value[RolePermissionFlags.EditAllArticles];
    }

    public async Task<bool> UserHasRoleAsync(string role)
    {
        var user = await GetUserAsync();
        return user is not null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> UserHasPermissionsAsync(RolePermissions rolePermissions)
    {
        (var user, var permissions) = await GetUserWithPermissionsAsync();

        if (user is null || permissions is null)
            return false;

        return permissions.Value[rolePermissions];
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
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return UserErrors.InvalidUserId;

        else if (!await UserHasPermissionsAsync(RolePermissionFlags.ManageRoles))
            return UserErrors.UnauthorizedAction;

        else if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var createRoleResult = await _roleManager.CreateAsync(new Role(roleName));
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
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return UserErrors.InvalidUserId;

        else if (!await UserHasPermissionsAsync(RolePermissionFlags.ManageRoles))
            return UserErrors.UnauthorizedAction;

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

    public async Task<(User?, RolePermissions?)> GetUserWithPermissionsAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.User is null)
            return (null, null);

        var user = await _userManager.GetUserAsync(httpContext.User);

        if (user is null)
            return (null, null);

        var permissions = await _userRepository.GetUserPermissionsByUserIdAsync(user.Id);

        if (permissions is null)
            return (null, null);
        return (user, permissions);
    }
}

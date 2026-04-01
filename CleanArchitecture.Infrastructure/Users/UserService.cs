using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Users;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Articles;
using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Infrastructure.Authentication.Authorization;
using CleanArchitecture.Infrastructure.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
    public const string PermissionsClaimType = "permissions";

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
        var permissions = await GetPermissionsAsync();

        if (permissions is null)
            return false;

        else if (permissions.Value[RolePermissionFlags.EditAllArticles])
            return true;

        var userId = await GetUserIdAsync();
        var article = await _articleRepository.GetArticleByIdAsync(articleId);

        if (article is null)
            return false;

        if (article.UserId == userId)
            return permissions.Value[RolePermissionFlags.EditOwnArticles];
        else return false;
    }

    public async Task<bool> UserHasPermissionsAsync(RolePermissions rolePermissions)
    {
        var permissions = await GetPermissionsAsync();

        if (permissions is null)
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

    public async Task<RolePermissions?> GetPermissionsAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User is null)
            return null;

        Predicate<Claim> permissionsPredicate = claim =>
               claim.Type == AuthorizationPolicies.AllPermissionsClaimType
            && !string.IsNullOrWhiteSpace(claim.Value);

        var hasPermissions = httpContext.User.HasClaim(permissionsPredicate);

        if (!hasPermissions)
            return null;

        var claim = httpContext.User.FindFirst(permissionsPredicate);
        if (claim?.Value is null)
            return null;

        else if (long.TryParse(claim.Value, out var permissionsNumber))
            return (RolePermissionFlags)permissionsNumber;

        else return null;
    }
}

using CleanArchitecture.Application.Authentication;
using CleanArchitecture.Application.Users;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Infrastructure.Authentication.Authorization;
using CleanArchitecture.Infrastructure.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CleanArchitecture.Infrastructure.Authentication;

public class AuthenticationService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IUserRepository userRepository,
    IHttpContextAccessor contextAccessor): IAuthenticationService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = contextAccessor;

    public async Task<ErrorMessage?> LoginUserAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
            return UserErrors.InvalidCredentials;

        var passwordSignInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!passwordSignInResult.Succeeded)
            return UserErrors.InvalidCredentials;

        var permissions = await _userRepository.GetUserPermissionsByUserIdAsync(user.Id);
        var permissionString = permissions is not null ? permissions.Value.ToString() : string.Empty;

        if (permissions is null || string.IsNullOrEmpty(permissionString))
            return GeneralErrors.UnexpectedFailure;

        var permissionsClaims = AuthorizationPolicies.ClaimsFromPermissions(permissions.Value);
        await _signInManager.SignInWithClaimsAsync(user, true, permissionsClaims);

        return null;
    }

    public async Task<RegisterUserResponse> RegisterUserAsync(string username, string email, string password)
    {
        var user = new User
        {
            UserName = username,
            Email = email,
            EmailConfirmed = true,
        };

        var creationResult = await _userManager.CreateAsync(user, password);

        if(creationResult.Succeeded)
            await _userManager.AddToRoleAsync(user, "Reader");

        var response = new RegisterUserResponse
        {
            Succeeded = creationResult.Succeeded,
            Errors = creationResult.Errors.Select(error => error.Description).ToList()
        };
        return response;
    }
}

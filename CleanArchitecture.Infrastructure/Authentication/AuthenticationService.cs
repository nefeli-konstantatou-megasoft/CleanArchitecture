using CleanArchitecture.Application.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> LoginUserAsync(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
        return result is not null && result.Succeeded;
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

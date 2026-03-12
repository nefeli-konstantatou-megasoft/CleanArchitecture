using CleanArchitecture.Domain.Users;
using CleanArchitecture.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Repositories;

public class UserRepository(UserManager<User> userManager) : IUserRepository
{
    private readonly UserManager<User> _userManager = userManager;
    public async Task<IUser?> GetUserByIdAsync(string userId)
        => await _userManager.FindByIdAsync(userId);
}

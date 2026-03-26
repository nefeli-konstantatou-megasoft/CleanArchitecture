using CleanArchitecture.Domain.Users;
using CleanArchitecture.Infrastructure.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

public class UserRepository(UserManager<User> userManager) : IUserRepository
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<List<IUser>> GetAllUsersAsync()
        => await _userManager.Users.Select(user => (IUser)user).ToListAsync();

    public async Task<IUser?> GetUserByIdAsync(string userId)
        => await _userManager.FindByIdAsync(userId);
}

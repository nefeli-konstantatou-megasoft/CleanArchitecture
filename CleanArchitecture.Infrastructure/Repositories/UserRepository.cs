using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Infrastructure.Roles;
using CleanArchitecture.Infrastructure.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

public class UserRepository(
    UserManager<User> userManager,
    ApplicationDbContext dbContext) : IUserRepository
{
    private readonly ApplicationDbContext _context = dbContext;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<List<IUser>> GetAllUsersAsync()
        => await _userManager.Users.Select(user => (IUser)user).ToListAsync();

    public async Task<IUser?> GetUserByIdAsync(string userId)
        => await _userManager.FindByIdAsync(userId);

    public async Task<RolePermissions?> GetUserPermissionsByUserIdAsync(string userId)
    {
        var userIdParameter = new SqlParameter("UserId", userId);
        var permissionsLong = await _context.Database.SqlQuery<long?>(
            @$"SELECT  
                SUM(DISTINCT(roles.Permissions & 0x01)) +
                SUM(DISTINCT(roles.Permissions & 0x02)) +
                SUM(DISTINCT(roles.Permissions & 0x04)) +
                SUM(DISTINCT(roles.Permissions & 0x08)) +
                SUM(DISTINCT(roles.Permissions & 0x10)) +
                SUM(DISTINCT(roles.Permissions & 0x20)) +
                SUM(DISTINCT(roles.Permissions & 0x40)) +
                SUM(DISTINCT(roles.Permissions & 0x80))
                AS Value
            FROM dbo.AspNetUserRoles userRoles
            INNER JOIN dbo.AspNetUsers users on users.Id = userRoles.UserId
            INNER JOIN dbo.AspNetRoles roles on roles.Id = userRoles.RoleId
            WHERE users.Id = {userIdParameter}"
        ).SingleOrDefaultAsync();
        return (RolePermissions?)(RolePermissionFlags?)permissionsLong;
    }
}

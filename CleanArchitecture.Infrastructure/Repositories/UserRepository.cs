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
                SUM(DISTINCT(roles.Permissions & 0x001)) +
                SUM(DISTINCT(roles.Permissions & 0x002)) +
                SUM(DISTINCT(roles.Permissions & 0x004)) +
                SUM(DISTINCT(roles.Permissions & 0x008)) +
                SUM(DISTINCT(roles.Permissions & 0x010)) +
                SUM(DISTINCT(roles.Permissions & 0x020)) +
                SUM(DISTINCT(roles.Permissions & 0x040)) +
                SUM(DISTINCT(roles.Permissions & 0x080)) +
                SUM(DISTINCT(roles.Permissions & 0x100)) +
                SUM(DISTINCT(roles.Permissions & 0x200)) + 
                SUM(DISTINCT(roles.Permissions & 0x400)) + 
                SUM(DISTINCT(roles.Permissions & 0x800))
                AS Value
            FROM dbo.AspNetUserRoles userRoles
            INNER JOIN dbo.AspNetUsers users on users.Id = userRoles.UserId
            INNER JOIN dbo.AspNetRoles roles on roles.Id = userRoles.RoleId
            WHERE users.Id = {userIdParameter}"
        ).SingleOrDefaultAsync();
        return (RolePermissions?)(RolePermissionFlags?)permissionsLong;
    }
}

using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Roles;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Roles;

public class RoleRepository(
    RoleManager<Role> roleManager,
    ApplicationDbContext dbContext) : IRoleRepository
{
    private readonly RoleManager<Role> _roleManager = roleManager;
    private readonly ApplicationDbContext _context = dbContext;

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    public async Task<IRole?> GetRoleAsync(string roleName)
    {
        return await _roleManager.FindByNameAsync(roleName);
    }

    public async Task<IRole?> CreateRoleAsync(string roleName, RolePermissions initialPermissions)
    {
        var result = await _roleManager.CreateAsync(new Role(roleName, initialPermissions));
        if (!result.Succeeded)
            return null;
        return await GetRoleAsync(roleName);
    }

    public async Task<bool> DeleteRoleAsync(string roleName)
    {
        var result = await _roleManager.DeleteAsync(new Role(roleName));
        return result.Succeeded;
    }

    public async Task<bool> ChangeRolePermissionsAsync(string roleName, RolePermissions newPermissions)
    {
        var role = await _roleManager.FindByNameAsync(roleName);

        if (role is null)
            return false;

        role.Permissions = newPermissions;
        var result = await _roleManager.UpdateAsync(role);
        return result.Succeeded;
    }
}

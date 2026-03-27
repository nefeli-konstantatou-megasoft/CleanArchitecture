using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Roles;

public interface IRoleRepository
{
    public Task<bool> RoleExistsAsync(string roleName);
    public Task<IRole?> GetRoleAsync(string roleName);
    public Task<IRole?> CreateRoleAsync(string roleName, RolePermissions initialPermissions);
    public Task<bool> DeleteRoleAsync(string roleName);
    public Task<bool> ChangeRolePermissionsAsync(string roleName, RolePermissions newPermissions);
}


using CleanArchitecture.Domain.Roles;

namespace CleanArchitecture.Application.Roles.ChangeRolePermissions;

public class ChangeRolePermissionsCommand(string name, RolePermissions permissions) : ICommand
{
    public string Name { get; set; } = name;
    public RolePermissions Permissions { get; set; } = permissions;
}

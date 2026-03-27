
using CleanArchitecture.Domain.Roles;

namespace CleanArchitecture.Application.Roles.CreateRole;

public class CreateRoleCommand(string name, RolePermissions permissions) : ICommand<RoleResponse>
{
    public string Name { get; set; } = name;
    public RolePermissions Permissions { get; set; } = permissions;
}

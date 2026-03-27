using CleanArchitecture.Domain.Roles;

namespace CleanArchitecture.Application.Roles;

public record struct RoleResponse(string Id, string Name, RolePermissions Permissions)
{
    public RolePermissions Permissions = Permissions;
}

using CleanArchitecture.Domain.Roles;

namespace CleanArchitecture.Infrastructure.Roles;

static class CoreRoles
{
    public static List<Role> Roles = typeof(CoreRolePermissions).GetFields()
        .Where(fieldInfo => fieldInfo.FieldType == typeof(RolePermissions))
        .Select(fieldInfo => new Role(fieldInfo.Name, (RolePermissions)fieldInfo.GetValue(null)!))
        .ToList();
}

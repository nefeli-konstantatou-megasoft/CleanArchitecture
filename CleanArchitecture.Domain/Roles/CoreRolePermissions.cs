namespace CleanArchitecture.Domain.Roles;

public static class CoreRolePermissions
{
    public static readonly RolePermissions Admin = RolePermissions.All;

    public static readonly RolePermissions Writer =
        RolePermissionFlags.CreateArticles | RolePermissionFlags.EditOwnArticles | RolePermissionFlags.DeleteOwnArticles;

    public static readonly RolePermissions Moderator =
        Writer | RolePermissionFlags.EditAllArticles | RolePermissionFlags.EditAllArticles;
}

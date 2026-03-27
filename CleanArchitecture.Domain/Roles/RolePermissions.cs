namespace CleanArchitecture.Domain.Roles;

[Flags]
public enum RolePermissionFlags : ulong
{
    ViewUnpublishedArticles = 1 << 0,
    CreateArticles = 1 << 1,
    EditOwnArticles = 1 << 2,
    DeleteOwnArticles = 1 << 3,
    EditAllArticles = 1 << 4,
    DeleteAllArticles = 1 << 5,
    PublishAllArticles = 1 << 6,
    ManageRoles = 1 << 7,
};

public struct RolePermissions
{
    private RolePermissionFlags _permissions;

    public static readonly RolePermissions All = new RolePermissions((RolePermissionFlags)long.MaxValue);
    public static readonly RolePermissions None = new RolePermissions((RolePermissionFlags)0);

    public RolePermissions this[RolePermissions otherPermissions]
    {
        readonly get => (_permissions & otherPermissions._permissions) == otherPermissions._permissions ? All : None;
        set => _permissions = (_permissions & ~otherPermissions) | (otherPermissions & value);
    }

    public static RolePermissions operator|(RolePermissions first, RolePermissions second) => new RolePermissions(first._permissions | second._permissions);
    public static RolePermissions operator&(RolePermissions first, RolePermissions second) => new RolePermissions(first._permissions & second._permissions);
    public static RolePermissions operator~(RolePermissions operand) => new RolePermissions(~operand._permissions);

    public static implicit operator RolePermissions(RolePermissionFlags permissionFlags) => new RolePermissions(permissionFlags);
    public static implicit operator RolePermissionFlags(RolePermissions permissions) => permissions._permissions;
    public static implicit operator bool(RolePermissions permissionFlags) => permissionFlags._permissions != 0;

    private RolePermissions(RolePermissionFlags permissionFlags)
    {
        _permissions = permissionFlags;
    }
}

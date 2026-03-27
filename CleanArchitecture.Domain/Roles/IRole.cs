namespace CleanArchitecture.Domain.Roles;

public interface IRole
{
    public string Id { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public RolePermissions Permissions { get; set; }
}

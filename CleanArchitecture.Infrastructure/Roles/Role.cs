using CleanArchitecture.Domain.Roles;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Infrastructure.Roles;

public class Role : IdentityRole<string>, IRole
{
    public Role(string name, RolePermissions initialPermissions) : base(name)
    {
        Permissions = initialPermissions;
        Id = Guid.NewGuid().ToString();
    }

    public Role(string name) : this(name, RolePermissions.None) { }

    public RolePermissions Permissions { get; set; }
}

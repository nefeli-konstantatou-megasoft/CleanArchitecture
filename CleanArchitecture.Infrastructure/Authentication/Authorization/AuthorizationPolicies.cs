using CleanArchitecture.Domain.Roles;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Infrastructure.Authentication.Authorization;

public static class AuthorizationPolicies
{
    public const string PermissionPolicyPrefix = "policy.permissions.";
    public const string PermissionClaimPrefix = "permissions.";
    public const string PermissionClaimType = "permissions";
    public const string AllPermissionsClaimType = "permissions.bitset";
    public static List<(string, Claim)> PermissionPolicies
        => _permissionPolicies is not null
            ? _permissionPolicies
            : _permissionPolicies =
                Enum.GetNames<RolePermissionFlags>()
                    .Select(name => (PermissionPolicyPrefix + name, new Claim(PermissionClaimType, PermissionClaimPrefix + name)))
                    .ToList();

    public static List<Claim> ClaimsFromPermissions(RolePermissions permissions)
    {
        var claims = Enum.GetValues<RolePermissionFlags>()
            .Where(value => permissions[value] != RolePermissions.None)
            .Select(permission => new Claim(PermissionClaimType, PermissionClaimPrefix + permission.ToString()))
            .ToList();
        claims.Add(new Claim(AllPermissionsClaimType, permissions.ToString()));
        return claims;
    }

    private static List<(string, Claim)>? _permissionPolicies = null;
}

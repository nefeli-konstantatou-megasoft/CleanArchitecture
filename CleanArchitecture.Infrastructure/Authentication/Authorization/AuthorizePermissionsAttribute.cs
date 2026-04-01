using CleanArchitecture.Application.Users;
using CleanArchitecture.Domain.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CleanArchitecture.Infrastructure.Authentication.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizePermissionsAttribute : AuthorizeAttribute
{
    public AuthorizePermissionsAttribute(RolePermissionFlags permissions)
    {
        Policy = AuthorizationPolicies.PermissionPolicyPrefix + permissions.ToString();
    }
}

using CleanArchitecture.Application.Authentication;
using CleanArchitecture.Application.Users;
using CleanArchitecture.Domain.Articles;
using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.Authentication.Authorization;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Infrastructure.Roles;
using CleanArchitecture.Infrastructure.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContextFactory<ApplicationDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );
        services.AddDbContext<ApplicationDbContext>();

        AddAuthentication(services);

        services.AddHttpContextAccessor();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    private static void AddAuthentication(IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
        services.AddCascadingAuthenticationState();

        services.AddAuthorization(options => {
            foreach (var permissionPolicy in AuthorizationPolicies.PermissionPolicies)
                options.AddPolicy(permissionPolicy.Item1, policy => {
                    policy.RequireClaim(permissionPolicy.Item2.Type, permissionPolicy.Item2.Value);
                });
        });

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        }).AddIdentityCookies();

        services.AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
    }

    public static async Task SetupPrimitiveRoles(this IServiceProvider serviceProvider)
    {
        var roleRepository = serviceProvider.GetService<IRoleRepository>()!;

        foreach(var coreRole in CoreRoles.Roles)
            await roleRepository.CreateRoleAsync(coreRole!.Name!, coreRole.Permissions);
    }
}

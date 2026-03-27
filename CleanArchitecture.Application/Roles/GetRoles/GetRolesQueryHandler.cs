using CleanArchitecture.Application.Users;
using CleanArchitecture.Domain.Roles;

namespace CleanArchitecture.Application.Roles.GetRoles;

public class GetRolesQueryHandler(
    IRoleRepository roleRepository,
    IUserService userService) : IQueryHandler<GetRolesQuery, List<RoleResponse>>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUserService _userService = userService;

    public async Task<Result<List<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        if (!await _userService.UserHasPermissionsAsync(RolePermissionFlags.ManageRoles))
            return Result<List<RoleResponse>>.Error(RoleErrors.UnauthorizedAction);

        var result = await _roleRepository.GetRolesAsync();
        var adapted = result.Adapt<List<RoleResponse>?>();

        return Result<List<RoleResponse>>.FromValue(adapted);
    }
}

using CleanArchitecture.Application.Users;
using CleanArchitecture.Domain.Roles;

namespace CleanArchitecture.Application.Roles.ChangeRolePermissions;

public class ChangeRolePermissionsCommandHandler(
    IRoleRepository roleRepository,
    IUserService userService) : ICommandHandler<ChangeRolePermissionsCommand>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUserService _userService = userService;

    public async Task<Result> Handle(ChangeRolePermissionsCommand request, CancellationToken cancellationToken)
    {
        if (!await _userService.UserHasPermissionsAsync(RolePermissionFlags.ManageRoles))
            return Result<RoleResponse>.Error(RoleErrors.UnauthorizedAction);

        var response = await _roleRepository.ChangeRolePermissionsAsync(request.Name, request.Permissions);

        return response ? Result.Ok() : Result.Error(RoleErrors.RoleNameNotFound);
    }
}

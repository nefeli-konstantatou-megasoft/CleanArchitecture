using CleanArchitecture.Application.Users;
using CleanArchitecture.Domain.Roles;

namespace CleanArchitecture.Application.Roles.DeleteRole;

public class DeleteRoleCommandHandler(
    IRoleRepository roleRepository,
    IUserService userService) : ICommandHandler<DeleteRoleCommand>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUserService _userService = userService;

    public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        if (!await _userService.UserHasPermissionsAsync(RolePermissionFlags.ManageRoles))
            return Result<RoleResponse>.Error(RoleErrors.UnauthorizedAction);

        var result = await _roleRepository.DeleteRoleAsync(request.Name);
        return result ? Result.Ok() : Result.Error(RoleErrors.RoleNameNotFound);
    }
}

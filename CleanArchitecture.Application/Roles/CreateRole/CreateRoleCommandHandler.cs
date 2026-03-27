using CleanArchitecture.Application.Users;
using CleanArchitecture.Domain.Roles;

namespace CleanArchitecture.Application.Roles.CreateRole;

public class CreateRoleCommandHandler(
    IRoleRepository roleRepository,
    IUserService userService) : ICommandHandler<CreateRoleCommand, RoleResponse>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUserService _userService = userService;

    public async Task<Result<RoleResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if (!await _userService.UserHasPermissionsAsync(RolePermissionFlags.ManageRoles))
            return Result<RoleResponse>.Error(RoleErrors.UnauthorizedAction);

        var response = await _roleRepository.CreateRoleAsync(request.Name, request.Permissions);
        return response is not null ? response.Adapt<RoleResponse>() : RoleErrors.RoleNameAlreadyExists;
    }
}

namespace CleanArchitecture.Application.Users.AddRoleToUser;

public class AddRoleToUserCommandHandler(
    IUserService userService) : ICommandHandler<AddRoleToUserCommand>
{
    private readonly IUserService _userService = userService;

    public async Task<Result> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userService.AddRoleToUserAsync(request.UserId, request.RoleName);
        return result?.Code != 0 ? Result.Error(result!) : Result.Ok();
    }
}

namespace CleanArchitecture.Application.Users.AddRoleToUser;

public class RemoveRoleFromUserCommandHandler(
    IUserService userService) : ICommandHandler<RemoveRoleFromUserCommand>
{
    private readonly IUserService _userService = userService;

    public async Task<Result> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userService.RemoveRoleFromUserAsync(request.UserId, request.RoleName);
        return result?.Code != 0 ? Result.Error(result!) : Result.Ok();
    }
}

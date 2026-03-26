using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Users.GetUsersQuery;

public class GetUsersQueryHandler(
    IUserRepository userRepository,
    IUserService userService) : IQueryHandler<GetUsersQuery, List<UserResponse>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserService _userService = userService;

    public async Task<Result<List<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        if(!await _userService.UserHasRoleAsync("Admin"))
            return Result<List<UserResponse>>.Error(UserErrors.UnauthorizedAction);

        var users = await _userRepository.GetAllUsersAsync();
        List<UserResponse> response = [];

        foreach(var user in users)
        {
            var userResponse = user.Adapt<UserResponse>();
            userResponse.Roles = await _userService.GetUserRolesByUserIdAsync(user.Id);
            response.Add(userResponse);
        }

        return response;
    }
}

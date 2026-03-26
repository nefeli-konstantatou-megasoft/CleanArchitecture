namespace CleanArchitecture.Application.Users.AddRoleToUser;

public class AddRoleToUserCommand(string userId, string roleName) : ICommand
{
    public string UserId { get; set; } = userId;

    public string RoleName { get; set; } = roleName;
}

namespace CleanArchitecture.Application.User.LoginUser;

public class LoginUserCommand(string username, string password) : ICommand
{
    public required string UserName { get; set; } = username;
    public required string Password { get; set; } = password;
}

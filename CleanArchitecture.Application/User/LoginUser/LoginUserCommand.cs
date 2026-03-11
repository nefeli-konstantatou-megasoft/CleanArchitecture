namespace CleanArchitecture.Application.User.LoginUser;

public class LoginUserCommand(string username, string password) : ICommand
{
    public string UserName { get; set; } = username;
    public string Password { get; set; } = password;
}

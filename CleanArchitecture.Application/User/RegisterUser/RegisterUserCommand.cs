namespace CleanArchitecture.Application.User.RegisterUser;

public class RegisterUserCommand(string username, string email, string password) : ICommand
{
    public string UserName { get; set; } = username;
    public string UserEmail { get; set; } = email;
    public string Password { get; set; } = password;
}

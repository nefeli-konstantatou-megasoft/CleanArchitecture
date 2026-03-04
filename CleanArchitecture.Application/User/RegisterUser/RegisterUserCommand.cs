namespace CleanArchitecture.Application.User.RegisterUser;

public class RegisterUserCommand(string username, string email, string password) : ICommand
{
    public required string UserName { get; set; } = username;
    public required string UserEmail { get; set; } = email;
    public required string Password { get; set; } = password;
}

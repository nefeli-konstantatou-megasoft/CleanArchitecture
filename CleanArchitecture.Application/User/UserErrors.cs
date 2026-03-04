namespace CleanArchitecture.Application.User;

public static class UserErrors
{
    public static readonly ErrorMessage InvalidCredentials = new(3000, "The given username and password combination given was incorrect.");
    public static readonly ErrorMessage RegisterFailed = new(3001, "Failed to register user.");
}

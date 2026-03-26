namespace CleanArchitecture.Application.Users;

public static class UserErrors
{
    public static readonly ErrorMessage InvalidCredentials = new(3000, "The given username and password combination given was incorrect.");
    public static readonly ErrorMessage RegisterFailed = new(3001, "Failed to register user.");
    public static readonly ErrorMessage InvalidUserId = new(3003, "The given user ID is invalid.");
    public static readonly ErrorMessage UnauthorizedAction = new(3002, "You are not authorized to perform this action.");
    public static readonly ErrorMessage UserAlreadyHasRole = new(3004, "The target user already has this role.");
    public static readonly ErrorMessage RoleNameNotFound = new(3005, "The given role name was not found.");
}

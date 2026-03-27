namespace CleanArchitecture.Application.Roles;

public static class RoleErrors
{
    public static readonly ErrorMessage RoleNameAlreadyExists = new(4000, "A role with the given name already exists.");

    public static readonly ErrorMessage RoleNameNotFound = new(4001, "The requested role name was not found.");

    public static readonly ErrorMessage UnauthorizedAction = new(4002, "You are not authorized to manage roles.");

    public static readonly ErrorMessage CoreRoleDeletion = new(4003, "Cannot delete a core role.");
}

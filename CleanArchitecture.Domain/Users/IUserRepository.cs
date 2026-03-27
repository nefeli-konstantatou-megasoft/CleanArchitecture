using CleanArchitecture.Domain.Roles;

namespace CleanArchitecture.Domain.Users;

public interface IUserRepository
{
    Task<List<IUser>> GetAllUsersAsync();
    Task<IUser?> GetUserByIdAsync(string userId);
    Task<RolePermissions?> GetUserPermissionsByUserIdAsync(string userId);
}

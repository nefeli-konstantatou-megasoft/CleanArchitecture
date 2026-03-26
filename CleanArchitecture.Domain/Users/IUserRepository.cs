namespace CleanArchitecture.Domain.Users;

public interface IUserRepository
{
    Task<List<IUser>> GetAllUsersAsync();
    Task<IUser?> GetUserByIdAsync(string userId);
}

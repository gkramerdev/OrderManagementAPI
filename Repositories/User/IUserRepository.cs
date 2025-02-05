using OrderManagementAPI.Models;

namespace OrderManagementAPI.Repositories.User
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<UserModel> CreateUserAsync(UserModel user);

        Task<List<UserModel>> GetAllUsersAsync();

        Task<UserModel> GetUserByIdAsync(int id);

    }
}

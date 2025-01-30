using OrderManagementAPI.Models;

namespace OrderManagementAPI.Repositories
{
    public interface IUserRepository 
    {
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<UserModel> CreateUserAsync(UserModel user);

    }
}

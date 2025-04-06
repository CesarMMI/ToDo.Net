
using api.Models;

namespace api.Interfaces;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> FindByEmailAsync(string email);
    Task<User?> FindByIdAsync(int id);
}

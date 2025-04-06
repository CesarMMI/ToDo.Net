using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User> CreateAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await FindByEmailAsync(email) is not null;
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> FindByIdAsync(int id)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}

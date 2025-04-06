using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Models.User> Users { get; set; }
    public DbSet<Models.List> Lists { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
}

public static class AppDbContextExtensions
{
    public static IServiceCollection UseSqlServer(this WebApplicationBuilder builder)
    {
        return builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
    }
}

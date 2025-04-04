using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Models.List> Lists { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
}
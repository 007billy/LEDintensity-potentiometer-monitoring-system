using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<LED> LED { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}

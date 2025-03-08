using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure;
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }
}

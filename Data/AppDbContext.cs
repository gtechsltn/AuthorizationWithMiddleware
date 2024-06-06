using Microsoft.EntityFrameworkCore;
using AuthorizationWithMiddleware.Models;

namespace AuthorizationWithMiddleware.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed a test user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "testuser",
                    Password = "testpassword" // In a real application, store passwords securely (e.g., hashed)
                }
            );
        }
    }
}

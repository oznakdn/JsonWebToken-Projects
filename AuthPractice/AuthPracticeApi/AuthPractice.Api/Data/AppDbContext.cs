using AuthPractice.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthPractice.Api.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Token> Tokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasData(
            new Product("IPhone13", 15_000, 50) { Id = 1 },
            new Product("Macbook Air", 20_000, 100) { Id = 2 },
            new Product("IPad Pro", 20_000, 150) { Id = 3 });

        modelBuilder.Entity<Role>()
            .HasData(
            new Role("Manager") { Id = 1 },
            new Role("Admin") { Id = 2 },
            new Role("User") { Id = 3 });

        modelBuilder.Entity<User>()
            .HasData(
            new User("johndoe", "john.doe@gmail.com", "john123") { Id = 1, RoleId = 1 },
            new User("jacksmith", "jack.smith@gmail.com", "jack123") { Id = 2, RoleId = 2 },
            new User("canycall", "cany.call@gmail.com", "cany123") { Id = 3, RoleId = 3 });

        base.OnModelCreating(modelBuilder);
    }
}


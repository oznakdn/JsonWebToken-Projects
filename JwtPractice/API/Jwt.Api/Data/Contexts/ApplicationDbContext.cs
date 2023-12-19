using Jwt.Api.Helpers;

namespace Jwt.Api.Data.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<ApplicationUser> Users { get; set; }
    public virtual DbSet<ApplicationRole> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Category>()
            .HasData(new Category("Electronics") { CategoryId = 100 });

        modelBuilder.Entity<Product>()
            .HasData(
            new Product(100, "Phone", 10000, 100) { ProductId = 1 },
            new Product(100, "Laptop", 30000, 50) { ProductId = 2 });

        modelBuilder.Entity<ApplicationUser>()
            .HasData(new ApplicationUser("John", "Doe", "john.doe@mail.com", PasswordHashHelper.HashPassword("john123")) { UserId = 1, RoleId = 12 });

        modelBuilder.Entity<ApplicationRole>()
            .HasData(
            new ApplicationRole("standard") { RoleId = 10 },
            new ApplicationRole("admin") { RoleId = 11 },
            new ApplicationRole("manager") { RoleId = 12 }
            );

        base.OnModelCreating(modelBuilder);
    }
}


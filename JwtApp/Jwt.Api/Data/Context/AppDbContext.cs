using Jwt.Api.Models.Entity;
using Jwt.Api.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Api.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>()
        .HasData
        (
            new Customer { Id = 1, FullName = "Quinn Baldwin", Phone = "(687) 487-7412", Email = "ut.nec@icloud.edu", Age = 23 },
            new Customer { Id = 2, FullName = "Shoshana Campbell", Phone = "(868) 245-2812", Email = "cras.pellentesque@outlook.couk", Age = 50 },
            new Customer { Id = 3, FullName = "India Johnson", Phone = "(394) 327-4827", Email = "consequat.dolor.vitae@outlook.org", Age = 39 },
            new Customer { Id = 4, FullName = "Odysseus Watkins", Phone = "(884) 344-2238", Email = "arcu.iaculis.enim@protonmail.net", Age = 27 },
            new Customer { Id = 5, FullName = "Germane Lynch", Phone = "(527) 785-7348", Email = "nulla@yahoo.org", Age = 32 },
            new Customer { Id = 6, FullName = "Myra Lott", Phone = "(681) 436-3166", Email = "risus.donec@aol.edu", Age = 22 },
            new Customer { Id = 7, FullName = "Cade Chase", Phone = "(406) 236-6085", Email = "malesuada.fames@hotmail.net", Age = 31 },
            new Customer { Id = 8, FullName = "Todd Kennedy", Phone = "(445) 870-7432", Email = "euismod.ac@protonmail.org", Age = 49 },
            new Customer { Id = 9, FullName = "Lunea Pennington", Phone = "(847) 885-3753", Email = "id.mollis.nec@hotmail.com", Age = 33 },
            new Customer { Id = 10, FullName = "Azalia Ellis", Phone = "(618) 483-2702", Email = "lectus.ante@icloud.com", Age = 42 }
        );

        modelBuilder.Entity<Role>()
        .HasData(
        new Role { Id = 1, RoleTitle = "SuperAdmin", RoleInformation = "This role has all processes." },
        new Role { Id = 2, RoleTitle = "Admin", RoleInformation = "This role has only get and create processes." },
        new Role { Id = 3, RoleTitle = "Standard", RoleInformation = "This role has only get process." }
        );

        modelBuilder.Entity<User>()
        .HasData(
        new User { Id = 1, Username = "john_doe", Password = "123456", Email = "john.doe@mail.com", RoleId = 1 },
        new User { Id = 2, Username = "joel_foster", Password = "123456", Email = "joel.foster@mail.com", RoleId = 2 },
        new User { Id = 3, Username = "elis_howard", Password = "123456", Email = "Elis.Howard@mail.com", RoleId = 3 },
        new User { Id = 4, Username = "lucas_mccarthy", Password = "123456", Email = "lucas.mccarthy@mail.com", RoleId = 3 },
        new User { Id = 5, Username = "andrew_stone", Password = "123456", Email = "andrew.stone@mail.com", RoleId = 3 }
        );
    }

}
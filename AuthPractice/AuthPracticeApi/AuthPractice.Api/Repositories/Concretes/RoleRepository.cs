using AuthPractice.Api.Data;
using AuthPractice.Api.Entities;
using AuthPractice.Api.Repositories.Contracts;

namespace AuthPractice.Api.Repositories.Concretes;
public sealed class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}


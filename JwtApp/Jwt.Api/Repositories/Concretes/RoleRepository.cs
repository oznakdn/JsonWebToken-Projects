using Jwt.Api.Data.Context;
using Jwt.Api.Models.Identity;
using Jwt.Api.Repositories.Abstracts;
using Jwt.Api.Repositories.Concretes.Common;

namespace Jwt.Api.Repositories.Concretes;

public class RoleRepository : GenericRepository<Role, AppDbContext>, IRoleRepository
{
    public RoleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
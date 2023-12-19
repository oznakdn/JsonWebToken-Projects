using AuthPractice.Api.Data;
using AuthPractice.Api.Entities;
using AuthPractice.Api.Repositories.Contracts;

namespace AuthPractice.Api.Repositories.Concretes;
public sealed class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

}


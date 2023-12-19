using AuthPractice.Api.Data;
using AuthPractice.Api.Entities;
using AuthPractice.Api.Repositories.Contracts;

namespace AuthPractice.Api.Repositories.Concretes;

public sealed class TokenRepository : GenericRepository<Token>, ITokenRepository
{
    public TokenRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}


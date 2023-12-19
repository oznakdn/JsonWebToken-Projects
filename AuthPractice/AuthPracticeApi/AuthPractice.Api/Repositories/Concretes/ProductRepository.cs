using AuthPractice.Api.Data;
using AuthPractice.Api.Entities;
using AuthPractice.Api.Repositories.Contracts;

namespace AuthPractice.Api.Repositories.Concretes;
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}


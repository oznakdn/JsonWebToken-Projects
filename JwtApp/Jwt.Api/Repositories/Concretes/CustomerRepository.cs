using Jwt.Api.Data.Context;
using Jwt.Api.Models.Entity;
using Jwt.Api.Repositories.Abstracts;
using Jwt.Api.Repositories.Concretes.Common;

namespace Jwt.Api.Repositories.Concretes;

public class CustomerRepository : GenericRepository<Customer, AppDbContext>, ICustomerRepository
{
    public CustomerRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
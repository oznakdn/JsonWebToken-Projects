using System.Linq.Expressions;
using Jwt.Api.Data.Context;
using Jwt.Api.Models.Base;
using Jwt.Api.Repositories.Abstracts.Common;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Api.Repositories.Concretes.Common;

public class GenericRepository<T, TContext> : IGenericRepository<T>
where T : Entity<int>
where TContext : DbContext
{
    protected readonly AppDbContext _dbContext;
    private readonly DbSet<T> _table;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.Set<T>();
    }

    public void Add(T entity)
    {
        _table.Add(entity);
        _dbContext.SaveChanges();
    }

    public void Delete(T entity)
    {
        _table.Remove(entity);
        _dbContext.SaveChanges();
    }

    public void Edit(T entity)
    {
        _table.Update(entity);
        _dbContext.SaveChanges();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null) => predicate != null ? await _table.Where(predicate).ToListAsync() : await _table.ToListAsync();

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate) => await _table.Where(predicate).FirstOrDefaultAsync();

    public async Task<IQueryable<T>> GetQueryableAsync(Expression<Func<T, bool>> predicate = null)
    {
        IQueryable<T> query = _table.AsQueryable();
        query = predicate != null ? _table.Where(predicate).AsQueryable() : query;
        return await Task.FromResult(query);
    }

}
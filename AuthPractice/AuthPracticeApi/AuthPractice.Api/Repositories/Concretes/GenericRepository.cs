using AuthPractice.Api.Data;
using AuthPractice.Api.Entities.Abstracts;
using AuthPractice.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthPractice.Api.Repositories.Concretes;
public abstract class GenericRepository<T> : IGenericRepository<T> where T : Entity<int>
{
    protected readonly AppDbContext _dbContext;
    private DbSet<T> _dbSet;
    protected GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public virtual void Insert(T entity) => _dbSet.Add(entity);

    public virtual void Update(T entity) => _dbSet.Update(entity);

    public virtual void Delete(T entity) => _dbSet.Remove(entity);

    public virtual async Task<T> GetAsync(int id) => await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));

    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet;
        query = predicate is null ? query : query.Where(predicate);
        if (includeProperties is not null)
        {
            foreach (var item in includeProperties)
            {
                query = query.Include(item);
            }
        }
        return await query.ToListAsync();
    }

    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet.Where(predicate);
        if (includeProperties is not null)
        {
            foreach (var item in includeProperties)
            {
                query = query.Include(item);
            }
        }
        return await query.SingleOrDefaultAsync();
    }

    public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
}


using AuthPractice.Api.Entities.Abstracts;
using System.Linq.Expressions;

namespace AuthPractice.Api.Repositories.Contracts;
public interface IGenericRepository<T> : IAsyncDisposable where T : Entity<int>
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
    Task<T> GetAsync(int id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> SaveAsync();
}


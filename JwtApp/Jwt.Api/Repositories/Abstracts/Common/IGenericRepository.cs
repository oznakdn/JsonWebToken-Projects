using System.Linq.Expressions;
using Jwt.Api.Models.Base;

namespace Jwt.Api.Repositories.Abstracts.Common;

public interface IGenericRepository<T> where T: Entity<int>
{
    void Add(T entity);
    void Edit(T entity);
    void Delete(T entity);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
    Task<IQueryable<T>> GetQueryableAsync(Expression<Func<T, bool>> predicate = null);
    Task<T>GetAsync(Expression<Func<T, bool>> predicate);
}
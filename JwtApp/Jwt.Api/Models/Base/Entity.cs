namespace Jwt.Api.Models.Base;

public abstract class Entity<TKey> : IEntity<TKey>
{
    public virtual TKey Id { get; set; }
}
namespace AuthPractice.Api.Entities.Abstracts;
public abstract class Entity<TKey> : IEntity<TKey>
{
    public virtual TKey Id { get; set; }
}


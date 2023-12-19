namespace Jwt.Api.Models.Base;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}
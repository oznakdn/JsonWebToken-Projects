

namespace Jwt.Api.Response.Contracts;

public interface IDataResponse<out T> where T : class
{
    T Value { get; }
    IEnumerable<T> Values { get; }

}

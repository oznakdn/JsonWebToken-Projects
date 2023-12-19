using Jwt.Api.Response.Contracts;

namespace Jwt.Api.Response.Concretes;

public class DataResponse<T> : Response, IDataResponse<T> where T : class
{

    public DataResponse(int statusCode, T value, string message) : base(statusCode, message)
    {
        Value = value;
    }

    public DataResponse(int statusCode, IEnumerable<T> values, string message) : base(statusCode, message)
    {
        Values = values;
    }

    public T Value { get; }
    public IEnumerable<T> Values { get; }
}

using AuthPractice.Api.Results.Contracts;

namespace AuthPractice.Api.Results.Concretes;
public class DataResult<T> : IDataResult<T>
{

    public DataResult(int statusCode, IEnumerable<T> results)
    {
        StatusCode = statusCode;
        Results = results;
        Success = true;
    }

    public DataResult(int statusCode, IEnumerable<T> results, string message)
    {
        StatusCode = statusCode;
        Results = results;
        Message = message;
        Success = true;
    }

    public DataResult(int statusCode, T result)
    {
        StatusCode = statusCode;
        Result = result;
        Success = true;
    }

    public DataResult(int statusCode, T result, string message)
    {
        StatusCode = statusCode;
        Result = result;
        Message = message;
        Success = true;
    }

    public DataResult(int statusCode, List<string> errors)
    {
        StatusCode = statusCode;
        Errors = errors;
        Success = false;
    }

    public DataResult(int statusCode, List<string> messages, bool success)
    {
        StatusCode = statusCode;
        Messages = messages;
        Success = success;
    }

    public DataResult(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
    public IEnumerable<T> Results { get; }

    public T Result { get; }

    public int StatusCode { get; }

    public string Message { get; }

    public List<string> Errors { get; }

    public bool Success { get; }

    public List<string> Messages { get; }
}


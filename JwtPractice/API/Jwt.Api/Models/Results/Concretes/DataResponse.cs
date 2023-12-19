namespace Jwt.Api.Models.Results.Concretes;

public record DataResponse<T> : IDataResponse<T>
{

    public DataResponse(T dataResult)
    {
        DataResult = dataResult;
        Success = true;
    }

    public DataResponse(T dataResult, string message)
    {
        DataResult = dataResult;
        Message = message;
        Success = true;
    }

    public DataResponse(IEnumerable<T> dataResults)
    {
        DataResults = dataResults;
        Success = true;
    }

    public DataResponse(IEnumerable<T> dataResults, string message)
    {
        DataResults = dataResults;
        Message = message;
        Success = true;
    }

    public DataResponse(IEnumerable<string> errors)
    {
        Errors = errors;
        Success = false;
    }

    public DataResponse(string message, bool success)
    {
        Message = message;
        Success = success;
    }

    public T DataResult { get; }
    public IEnumerable<T> DataResults { get; }
    public IEnumerable<string> Errors { get; }
    public string Message { get; }
    public bool Success { get; }
}

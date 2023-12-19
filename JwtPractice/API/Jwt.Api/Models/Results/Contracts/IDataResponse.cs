namespace Jwt.Api.Models.Results.Contracts;

public interface IDataResponse<T>:IResponse
{
    T DataResult { get;}
    IEnumerable<T> DataResults { get; }
}


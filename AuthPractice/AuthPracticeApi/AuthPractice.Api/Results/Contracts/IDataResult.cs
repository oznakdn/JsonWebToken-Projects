namespace AuthPractice.Api.Results.Contracts;
public interface IDataResult<T>:IResult
{
    public IEnumerable<T> Results { get; }
    public T Result { get; }
}


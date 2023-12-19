namespace AuthPractice.Api.Results.Contracts;
public interface IResult
{
    public int StatusCode { get; }
    public string Message { get; }
    public List<string> Messages { get; }
    public List<string> Errors { get; }
    public bool Success { get; }
}


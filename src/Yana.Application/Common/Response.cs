namespace Yana.Application.Common;

public abstract class Response
{
    public ErrorCode? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}

public class Response<T> : Response
{
    public T? Result { get; set; }
}
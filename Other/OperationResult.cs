namespace BookRest.Other;

public class OperationResult<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }

    public static OperationResult<T> Fail(string error) => new OperationResult<T>
    {
        Success = false,
        ErrorMessage = error 
    };
    public static OperationResult<T> Ok(T data) => new OperationResult<T>
    {
        Success = true,
        Data = data
    };
}
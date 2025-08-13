namespace Auth_Box.Models;

public class APIResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }

    public APIResponse(bool success, string message, T? data = default)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static APIResponse<T> Ok(T data, string message = "Success")
        => new APIResponse<T>(true, message, data);

    public static APIResponse<T> Fail(string message)
        => new APIResponse<T>(false, message);
}
namespace api.Dtos;

public abstract class BaseResponse<T>
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}

public class BaseResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}

namespace WebApplication1;

public class ReturnResult<T>
{
    public bool IsSuccess { get; set; }
    public T? Result { get; set; }
    public List<string> ErrorMessage { get; set; } = new();
}
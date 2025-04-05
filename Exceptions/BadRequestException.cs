namespace api.Exceptions;

public class BadRequestException(string message) : AppException(message)
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
}

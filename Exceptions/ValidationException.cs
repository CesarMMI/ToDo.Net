namespace api.Exceptions;

public class ValidationException(string message) : BadRequestException(message)
{
}

namespace api.Exceptions;

public class InternalServerErrorException() : AppException("Internal Server Error")
{
    public override int StatusCode => StatusCodes.Status500InternalServerError;
}
